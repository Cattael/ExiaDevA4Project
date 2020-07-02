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
    
    
   // private  Filees filees = new Filees();
    

    
    private String xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><file token=\"LXRIPMFUOSKGCHD\" name=\"2.txt\"><content>HS9jMCIjMGM7L2MlPikrKjI4Y3E=</content><key>WJCC</key></file>";
    
    
    public Boolean postFiles(String xmlMessage) 
    {
       

        
         try {
             sendJMSFiles(xmlMessage);
         } catch (JAXBException ex) {
             Logger.getLogger(DecryptServiceBean.class.getName()).log(Level.SEVERE, null, ex);
         }
         
        return true;
        
    }
    
    

    /*
    * methode pour la jms queux
    */
        private void sendJMSFiles (String message) throws JAXBException{

   
            /*
             // genere tous apartir de la classe
            JAXBContext jaxbContext = JAXBContext.newInstance(File.class);
            //création d'un Marshaller pour transfomer l'objet Java en flux XML
            Marshaller jaxbMarshaller = jaxbContext.createMarshaller();
            StringWriter writer = new StringWriter();
            
            //transformation de l'objet en flux XML stocké dans un Writer
            jaxbMarshaller.marshal(fichier, writer);
            String xmlMessage = writer.toString();
            //affichage du XML dans la console de sortie
            System.out.println(xmlMessage);*/
            
            //encapsulation du paiement au format XML dans un objet javax.jms.TextMessage
            TextMessage msg = context.createTextMessage(xml);
            
            context.createProducer().send(paymentQueue, msg);

    }
}
