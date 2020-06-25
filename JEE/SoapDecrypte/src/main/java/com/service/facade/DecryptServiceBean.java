/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.service.facade;

import java.io.File;
import java.io.StringWriter;
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
    
    
    
    
    @Override
    public Boolean isFrench(String text, int file) 
    {
        
        sendPayment(file);
       return true; 
    }
    
    
    
    /*
    * methode pour la jms queux
    */
        private void sendPayment(int payment){
        //utilisation de l'API JAX-B de gestion de flux pour marshaller (transformer) l'objet //Payment en chaine XML
       /*  JAXBContext jaxbContext;
        try {*/
            //obtention d'une instance JAXBContext associée au type Payment annoté avec JAX-B
          /*   jaxbContext = JAXBContext.newInstance();
            //création d'un Marshaller pour transfomer l'objet Java en flux XML
            Marshaller jaxbMarshaller = jaxbContext.createMarshaller();
            
            StringWriter writer = new StringWriter();
            
            //transformation de l'objet en flux XML stocké dans un Writer
            jaxbMarshaller.marshal(payment, writer);
            String xmlMessage = writer.toString();
                */
            //affichage du XML dans la console de sortie
            System.out.println(payment);
            String xmlStr = "<employees>" + 
                                "   <employee id=\"101\">" + 
                                "        <name>Lokesh Gupta</name>" + 
                                "       <title>Author</title>" + 
                                "   </employee>" + 
                                "   <employee id=\"102\">" + 
                                "        <name>Brian Lara</name>" + 
                                "       <title>Cricketer</title>" + 
                                "   </employee>" + 
                                "</employees>";
            
            //encapsulation du paiement au format XML dans un objet javax.jms.TextMessage
            TextMessage msg = context.createTextMessage(xmlStr);
            
            //envoi du message dans la queue paymentQueue
            context.createProducer().send(paymentQueue, msg);

            
            

     /*   } catch (JAXBException ex) {
            Logger.getLogger(DecryptServiceBean.class.getName()).log(Level.SEVERE, null, ex);
        }*/
    }
        

    // Add business logic below. (Right-click in editor and choose
    // "Insert Code > Add Business Method")
}
