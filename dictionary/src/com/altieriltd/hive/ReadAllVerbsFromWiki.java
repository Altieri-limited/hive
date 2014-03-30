package com.altieriltd.hive.dictionary;

import net.htmlparser.jericho.*;
import java.util.*;
import java.io.*;
import java.net.*;
import java.util.regex.Pattern;
import java.util.regex.Matcher;

public class ReadAllVerbsFromWiki {
    static final TreeSet<String> allWords= new TreeSet<String>();

    public static void main(String[] args) throws Exception {
        MicrosoftConditionalCommentTagTypes.register();
        PHPTagTypes.register();
        PHPTagTypes.PHP_SHORT.deregister();
        MasonTagTypes.register();
        ReadVerbFromWiki parser = new ReadVerbFromWiki();
        char initial = 'a';
        while (initial <= 'z') {
            findVerbs(initial++);
        }
        printAllWords();
    }
}
