<?php
    $text1 = 'hello world!';
    #并置运算符
    echo $text1 . 'what a nice day';
    echo '<br />';
    #strlen获取长度
    echo strlen($text1);
    echo '<br />';
    #strpos()在字符串来查找一个或一段指定字符。找到返回第一个匹配的位置。找不到返回false
    echo strpos($text1,"world");
    $a = strpos($text1,"a");
    $b = strpos($text1,"wa");
    echo '<br />';


    echo $text1;
?>