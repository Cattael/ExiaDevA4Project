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
    public Boolean postFiles(String[][] file, String token) 
    {
        System.out.print(file[1][1]);
       String xml = XmlGenerator(file);
        xml+="<token>"+token+"</token>";
        System.out.print("passe");
        sendJMSFiles(xml);
        
        return true;
    
    }
    
    
    
    /*
    * methode pour la jms queux
    */
        private void sendJMSFiles (String Xml){

            System.out.println(Xml);
           /* String xmlStr = "<employees>" + 
                                "   <employee id=\"101\">" + 
                                "        <name>Lokesh Gupta</name>" + 
                                "       <title>Author</title>" + 
                                "   </employee>" + 
                                "   <employee id=\"102\">" + 
                                "        <name>Brian Lara</name>" + 
                                "       <title>Cricketer</title>" + 
                                "   </employee>" + 
                                "</employees>"; */
            
            //encapsulation du paiement au format XML dans un objet javax.jms.TextMessage
            TextMessage msg = context.createTextMessage(Xml);
            
            //envoi du message dans la queue paymentQueue
            context.createProducer().send(paymentQueue, msg);

     /*   } catch (JAXBException ex) {
            Logger.getLogger(DecryptServiceBean.class.getName()).log(Level.SEVERE, null, ex);
        }*/
    }
        
    private String XmlGenerator(String[][] files) {
        
        String xmlStr="<files>";
        for( String[] file : files ) 
        {
            xmlStr+="<file>"+
                "<name>"+file[0]+"</name>" + 
                 "<content>"+file[1]+"</content>"+
                  "</file>";
                           
        }     
        xmlStr+="</files>";
        return xmlStr;
    }
}
