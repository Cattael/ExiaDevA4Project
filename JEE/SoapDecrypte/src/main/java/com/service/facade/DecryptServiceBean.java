/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.service.facade;


import com.entity.Filees;
import com.entity.File;
import java.io.StringReader;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.annotation.Resource;
import javax.ejb.Stateless;
import javax.ejb.LocalBean;
import javax.inject.Inject;
import javax.jms.JMSContext;
import javax.jms.Queue;
import javax.jms.TextMessage;
import javax.jws.WebService;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.Unmarshaller;

/**
 *
 * @author coren
 */
@Stateless
@WebService(
endpointInterface = "com.service.facade.DecryptServiceEndPointInterface",
portName = "DecryptPort", 
serviceName = "DecryptService"
 )

public class DecryptServiceBean implements DecryptServiceEndPointInterface  {

     @Inject //paquetage javax.inject
    private JMSContext context; //paquetage javax.jms
        
    @Resource(lookup = "jms/dicoQueue") //paquetage javax.annotation
    private Queue paymentQueue; //paquetage javax.jms
    
    
    private  Filees filees = new Filees();
    
    
    @Override
    public Boolean postFiles(String[][] files, String token) 
    {
        filees.setFilees(new ArrayList<File>());
        
        for( String[] file : files ) 
        {
            File f = new File();
            f.setTitle(file[0]);
            f.setContent(file[1]);
            filees.getFilees().add(f);
        }    
        
         try {
             sendJMSFiles();
         } catch (JAXBException ex) {
             Logger.getLogger(DecryptServiceBean.class.getName()).log(Level.SEVERE, null, ex);
         }
        return true;
    
    }
    
    

    /*
    * methode pour la jms queux
    */
        private void sendJMSFiles () throws JAXBException{

            
       
            JAXBContext jaxbContext;
     
            //obtention d'une instance JAXBContext associée au type Payment annoté avec JAX-B
            jaxbContext = JAXBContext.newInstance(Filees.class);
            //création d'un Marshaller pour transfomer l'objet Java en flux XML
            Marshaller jaxbMarshaller = jaxbContext.createMarshaller();
            
            StringWriter writer = new StringWriter();
            
            //transformation de l'objet en flux XML stocké dans un Writer
            jaxbMarshaller.marshal(filees, writer);
            
            
            
            String xmlMessage = writer.toString();
            //affichage du XML dans la console de sortie
            System.out.println(xmlMessage);
          
           
            

            TextMessage msg = context.createTextMessage(xmlMessage);
            
             unMarshalingExample(xmlMessage);
            
            //envoi du message dans la queue paymentQueue
            context.createProducer().send(paymentQueue, msg);
    }

    private void unMarshalingExample(String xmlMessage) throws JAXBException 
    {
        JAXBContext jaxbContext = JAXBContext.newInstance(Filees.class);
        Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
        StringReader reader = new StringReader(xmlMessage);
        System.out.print(xmlMessage);
            
        Filees emps = (Filees) jaxbUnmarshaller.unmarshal(reader);
        
        emps.getFilees();       
        
    }


}
