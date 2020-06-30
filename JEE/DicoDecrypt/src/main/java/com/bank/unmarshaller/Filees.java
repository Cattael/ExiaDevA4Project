/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package com.bank.unmarshaller;

import java.util.List;
 
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
 
@XmlRootElement(name = "filees")
@XmlAccessorType (XmlAccessType.FIELD)
public class Filees 
{
    
    @XmlAttribute(name = "token")
    private String text  ="gezgzegzeg";
    
    @XmlAttribute(name = "name")
    private String name  ="gezgzegzeg";
    
    
    @XmlElement(name = "file")
    private List<File> file = null;
 
    public List<File> getFilees() {
        return file;
    }
 
    public void setFilees(List<File> file) {
        this.file = file;
    }
}
