<?php
    #global $GLOBALS
    $a = 5;
    function text()
    {
        $b = 10;
        echo $GLOBALS['a']+$b;
        echo "<br />";
    }
    function text2()
    {
        global $a;
        $b = 20;
        echo $a+$b;
        echo "<br />";
    }
    text();
    text2();
    echo $a;
    #static
    function text3()
    {
        static $c=30;
        echo $c . "<br />";
        $c++;
    }
    text3();
    text3();
    text3();
    echo "<br />";
    #param
    function text4($d)
    {
        echo $d;
    }
    text4(6);
?>