package com.entity;
import java.io.Serializable;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "file")
@XmlAccessorType(XmlAccessType.FIELD)
public class File implements Serializable{
    
    @XmlElement
    private String content;

    @XmlElement
    private String title;

   
    
    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
    
       
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }


    
 
}
