package com.altieriltd.hive.dictionary;

import net.htmlparser.jericho.Source;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.net.URL;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by herschel on 23/03/14.
 */
public class ReadVerbFromWiki {
    String prefix = "https://it.wiktionary.org/w/index.php?title=Speciale%3APrefissi&prefix=Coniugazioni%2FItaliano%2F";
    String suffix = "&namespace=100";
    final String REG_EXP_LETTERS = "[a-zA-Z]*";
    final Pattern ONLY_LETTERS = Pattern.compile(REG_EXP_LETTERS);

    void printAllWords () {
        BufferedWriter writer = null;
        try {
            File file = new File("AllVerbs.txt");

            System.out.println(file.getCanonicalPath());

            writer = new BufferedWriter(new FileWriter(file));
            for (String word : allWords) {
                writer.write(word.toLowerCase());
                writer.write('\n');
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                writer.close();
            } catch (Exception e) {
            }
        }
    }

    void findVerbs(char initial) throws Exception {
        String page = prefix + initial + suffix;
        final Pattern verbs = Pattern.compile("Coniugazioni/Italiano/" + initial + REG_EXP_LETTERS);
        Source source = new Source(new URL(page));
        final String[] split = source.getTextExtractor().setIncludeAttributes(false).toString().split(" ");
        for (String word: split) {
            Matcher m = verbs.matcher(word);
            if (m.matches()) {
                addWords(m.group(0));
            }
        }
    }

    void addWords(String verb) throws Exception {
        String page =  "https://it.wiktionary.org/wiki/Appendice:" + verb;

        Source source = new Source(new URL(page));
        System.out.println(verb);

        final String[] split = source.getTextExtractor().setIncludeAttributes(false).toString().split(" ");
        for (String word: split) {
            Matcher m = ONLY_LETTERS.matcher(word);
            if (m.matches()) {
                allWords.add(word);
            }
        }

    }

}
