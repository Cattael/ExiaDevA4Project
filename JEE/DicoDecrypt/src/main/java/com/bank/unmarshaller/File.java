package com.bank.unmarshaller;
import java.io.Serializable;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "file")
@XmlAccessorType(XmlAccessType.FIELD)
public class File implements Serializable{
    
    
    @XmlAttribute
     private String token;
    
    
     @XmlAttribute
     private String name;
    
    @XmlElement
    private String content;

    @XmlElement
    private String key;
    
    @XmlElement
    private int probability;

    @XmlElement
    private String secretinfo;

   
    
    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
    
       
    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }
    
    
      public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
    
       public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
    
    public int getProbability() {
        return probability;
    }

    public void setProbability(int probability) {
        this.probability = probability;
    }
    
        public String getSecretinfo() {
        return secretinfo;
    }

    public void setSecretinfo(String secretinfo) {
        this.secretinfo = secretinfo;
    }






    
 
}
