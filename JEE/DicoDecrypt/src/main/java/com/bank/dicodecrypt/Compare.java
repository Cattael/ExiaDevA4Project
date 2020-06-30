/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package com.bank.dicodecrypt;

import java.util.ArrayList;
import java.util.List;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
;
/**
 *
 * @author alexi
 */
public class Compare {
	private int nombreDeMots = 0;
	private int nombreMax = 80;

	private ArrayList<String> wordArrayList = new ArrayList<String>();

	public Compare() {

	}

	public String Reader(String filename) {
		StringBuilder content = new StringBuilder();
		String line;
		BufferedReader bufferedreader = null;
		FileReader filereader = null;
		System.out.println(filename);
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

	public Double Comparaison(String Text, List<String> mots) {
		Double taux = 0.0;
		this.nombreDeMots = 0;
		this.wordArrayList.clear();

		for (String word : Text.split(" ")) {
			this.nombreDeMots++;
			if (isValidWord(word)) {
				if (compareStringToBDD(word, mots)) {
					this.wordArrayList.add(word);
				}
			}

		}
		System.out.println("Nombre de mots correspondants: " + this.wordArrayList.size());
		System.out.println("Nombre de mots total : " + this.nombreDeMots);

		if (!this.wordArrayList.isEmpty() && this.nombreDeMots != 0) {
			taux = ((double) this.wordArrayList.size() / (double) this.nombreDeMots) * 100;
		}
		System.out.println("Le taux est de " + taux + "%");
		return taux;
	}

	public Boolean compareStringToBDD(String motFichier, List<String> mots) {
		motFichier = motFichier.toLowerCase();

		for (String motBdd : mots) {
			if (motFichier.equals(motBdd)) {
				System.out.println("Ce mot est dans la base de données : " + motFichier);
				return true;
			}
		}
		return false;
	}

	public Boolean isValidWord(String word) {
		int letterNumber = 0;
		for (int i = 0; i < word.length(); i++) {
			if (word.charAt(i) != ' ') {
				++letterNumber;
			}
		}
		if (letterNumber > 1) {
			return true;
		} else {
			return false;

		}
	}
}

