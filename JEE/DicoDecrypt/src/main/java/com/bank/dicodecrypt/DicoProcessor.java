package com.bank.dicodecrypt;

import com.bank.unmarshaller.File;
import com.bank.unmarshaller.Filees;
import java.io.StringReader;
import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.TextMessage;
import javax.jms.BytesMessage;
import javax.jms.JMSException;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;


@MessageDriven(activationConfig = {
    @ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "jms/dicoQueue"),
    @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})

public class DicoProcessor implements MessageListener {
    	private static final String FILENAME1 = "E:\\Albatros.txt";
	private static final String FILENAME2 = "E:\\Random.txt";
	private static final String FILENAME3 = "E:\\Secret.txt";

	private List<String> mots =  new ArrayList<String>() ;
    public DicoProcessor() {
    }
    
    @Override
    public void onMessage(Message message) {
        
                try {
                    
                    TextMessage textMessage = (TextMessage) message;
                    String text = textMessage.getText();
                    
                    System.out.print(text);
                    System.out.print("plop");
                    try {
                        
                         unMarshalingExample(text);
                        
                       /* try {
                        
                        if (message instanceof BytesMessage) {
                        
                        BytesMessage bytesMessage = (BytesMessage) message;
                        byte[] data = new byte[(int) bytesMessage.getBodyLength()];
                        bytesMessage.readBytes(data);
                        System.out.print("Message received {}" + new String(data));
                        
                        } else if (message instanceof TextMessage) {
                        
                        TextMessage textMessage = (TextMessage) message;
                        String text = textMessage.getText();
                        ConnectDB database = new ConnectDB();
                        this.mots = database.getListMot();
                        checkTaux();
                        System.out.print("Message received {}" + text);
                        }
                        
                        } catch (JMSException jmsEx) {
                        jmsEx.printStackTrace();
                        }
                        
                        }
                        
                        
                        public boolean checkTaux() {
                        double tauxReference = 50.0;
                        Compare compare = new Compare();
                        double compared = compare.Comparaison(compare.Reader(FILENAME2), mots);
                        if(compared > tauxReference) {
                        System.out.println("C'est Win");
                        return true;
                        }
                        else {
                        System.out.println("C'est lose");
                        return false;
                        
                        }*/
                    } catch (JAXBException ex) {
                        Logger.getLogger(DicoProcessor.class.getName()).log(Level.SEVERE, null, ex);
                    }
                    
                } catch (JMSException ex) {
                    Logger.getLogger(DicoProcessor.class.getName()).log(Level.SEVERE, null, ex);
                }

	}


    private void unMarshalingExample(String xmlMessage) throws JAXBException 
    {
        JAXBContext jaxbContext = JAXBContext.newInstance(File.class);
        Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
        StringReader reader = new StringReader(xmlMessage);
        System.out.print(xmlMessage);
            
        File emps = (File) jaxbUnmarshaller.unmarshal(reader);
        
        System.out.print(emps.getContent());
        
    }
}
    

