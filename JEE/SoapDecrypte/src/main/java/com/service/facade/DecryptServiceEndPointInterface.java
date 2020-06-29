/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.service.facade;

import java.io.File;
import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;

/**
 *
 * @author coren
 */
@WebService(name = "DecryptEndPoint")
public interface DecryptServiceEndPointInterface 
{
     @WebMethod(operationName = "DecryptOperation")
     @WebResult(name = "acceptedDecrypt")

     Boolean postFiles(@WebParam(name="FileDecrypt") String[][] files, @WebParam(name="Token") String Token);
     
}
