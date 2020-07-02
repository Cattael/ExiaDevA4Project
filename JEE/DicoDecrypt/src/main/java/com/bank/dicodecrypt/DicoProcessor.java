package com.bank.dicodecrypt;

import com.bank.unmarshaller.File;
import com.bank.unmarshaller.Filees;
import java.io.StringReader;
import java.io.StringWriter;

import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.TextMessage;
import javax.jms.BytesMessage;
import javax.jms.JMSException;
import java.util.ArrayList;
import java.util.Base64;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.Unmarshaller;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;


@MessageDriven(activationConfig = {
    @ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "jms/dicoQueue"),
    @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})

public class DicoProcessor implements MessageListener {

	private List<String> mots =  new ArrayList<String>() ;
        private String receivedText;
    

        public DicoProcessor() {
        
        }
    
    @Override
    public void onMessage(Message message) {

        try {

            if (message instanceof BytesMessage) {

            BytesMessage bytesMessage = (BytesMessage) message;
            byte[] data = new byte[(int) bytesMessage.getBodyLength()];
            bytesMessage.readBytes(data);
            System.out.print("Message received {}" + new String(data));

            } else if (message instanceof TextMessage) {

            TextMessage textMessage = (TextMessage) message;
            String text = textMessage.getText();
            ConnectDB database = new ConnectDB();
            this.mots = database.getMots();

            System.out.print("Message received {}" + text);
            
            try{
                unMarshaling(text);
            }

            catch (JAXBException ex ) {
                ex.printStackTrace();
            }
            

               }} catch (JMSException jmsEx) {
            jmsEx.printStackTrace();
            }
       
    }


            public boolean checkTaux(String txt) {
		double tauxReference = 50.0;
		Compare compare = new Compare();
		double compared = compare.Comparaison(txt, mots);
                System.out.println(txt);
		if(compared > tauxReference) {
		  System.out.println("C'est Win");
		  return true;
	  }
		else {
		System.out.println("C'est lose");	
		return false;
	}
}


    private void unMarshaling(String xmlMessage) throws JAXBException 
    {
        JAXBContext jaxbContext = JAXBContext.newInstance(File.class);
        Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();

        StringReader reader = new StringReader(xmlMessage);            
        File emps = (File) jaxbUnmarshaller.unmarshal(reader);
        

        checkTaux(emps.getContent());

        byte[] decodedBytes = Base64.getDecoder().decode(emps.getContent());
        String decodedString = new String(decodedBytes);
        emps.setContent(decodedString);
        System.out.print(emps.getContent());
        
    }
    
    private String Marshaling(File file) throws JAXBException 
    {
             JAXBContext jaxbContext = JAXBContext.newInstance(File.class);
            //création d'un Marshaller pour transfomer l'objet Java en flux XML
            Marshaller jaxbMarshaller = jaxbContext.createMarshaller();
            StringWriter writer = new StringWriter();
            
            //transformation de l'objet en flux XML stocké dans un Writer
            jaxbMarshaller.marshal(file, writer);
            String xmlMessage = writer.toString();
            return xmlMessage;
    }

}
    

