/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package com.bank.dicodecrypt;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;


/**
 *
 * @author alexi
 */
public class ConnectDB {
	private Connection connection;

	
	public ConnectDB(){
        try{
           Class.forName("com.mysql.cj.jdbc.Driver");
           String url = "jdbc:mysql://localhost:3306/jeeproject?useUnicode=true&useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=UTC";
           String user = "root";
           String passwd ="Az3rP0iu";
           this.connection = DriverManager.getConnection(url, user, passwd);
       }catch(Exception e){
           try {
               System.out.println(e.getMessage());
           } catch (Exception ex) {
               java.util.logging.Logger.getLogger(ConnectDB.class.getName()).log(Level.SEVERE, null, ex);
           }
       }
   }
	
	public List<String> getListMot(){        
        List<String> arr = new ArrayList<String>();
        
        try {
            Statement st = this.connection.createStatement();
            ResultSet listeMots = st.executeQuery("SELECT * FROM mot");
            
            while(listeMots.next()){
                arr.add(listeMots.getString( "mot" ));
            }
        } catch (Exception e) {
            System.out.println(e);
        }
        return arr;
    }


}