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
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;



/**
 *
 * @author alexi
 */
public class ConnectDB {
	private Connection connection;
        private static final String FILENAME1 = "E:\\repProjet\\ExiaDevA4Project\\JEE\\DicoDecrypt\\src\\main\\java\\com\\bank\\dicodecrypt\\liste_francais.txt";

	
	public ConnectDB(){
        /*try{
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
       }*/
   }

	/*public List<String> getListMot(){        
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
    }*/

	public String Reader(String filename) {
		StringBuilder content = new StringBuilder();
		String line;
		BufferedReader bufferedreader = null;
		FileReader filereader = null;
		try {
			filereader = new FileReader(filename);
			bufferedreader = new BufferedReader(filereader);
			while ((line = bufferedreader.readLine()) != null) {
				content.append(line);
				content.append(System.lineSeparator());
			}

		} catch (IOException e) {
			e.printStackTrace();
		}
		return content.toString();
	}

        public List<String> getMots(){
           String oui =  Reader(FILENAME1);
           ArrayList<String> wordArrayList = new ArrayList<String>();
            for (String word : oui.split("\\r?\\n")) {
               wordArrayList.add(word);                 
		}

           return wordArrayList;
        }


}