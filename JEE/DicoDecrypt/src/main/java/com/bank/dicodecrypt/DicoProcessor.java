package com.bank.dicodecrypt;

import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.Message;
import javax.jms.MessageListener;

@MessageDriven(activationConfig = {
    @ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "jms/dicoQueue"),
    @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})
public class DicoProcessor implements MessageListener {
    
    public DicoProcessor() {
    }
    
    @Override
    public void onMessage(Message message) {
        
        System.out.print(message);
    }
    
}
    

