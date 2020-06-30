package com.bank.dicodecrypt;

import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.TextMessage;
import javax.jms.BytesMessage;
import javax.jms.JMSException;
import java.util.ArrayList;
import java.util.List;


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

		}
	}


    private void unMarshalingExample(String xmlMessage) throws JAXBException 
    {
        JAXBContext jaxbContext = JAXBContext.newInstance(Filees.class);
        Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
        StringReader reader = new StringReader(xmlMessage);
        System.out.print(xmlMessage);
            
        Filees emps = (Filees) jaxbUnmarshaller.unmarshal(reader);
        
        emps.getFilees();
        
        
        System.out.print(emps.getFilees().get(0).getContent());
        
    }
}
    

