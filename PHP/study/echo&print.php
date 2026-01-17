<?php
    $a = 'text1';
    $b = 'text2';
    $c = array('c1',2,'c3');

    echo $a."<br />";
    echo $a,"<br />";
    echo "what $b"."<br />";
    echo "yes {$c[2]}"."<br />";
    #单引号和双引号的区别,单引号是完全字符串
    echo 'what $b'."<br />";

    #print输出有返回值1
    print $a."<br />";
    print "what $b"."<br />";
    print "yes {$c[2]}"."<br />";
    #单引号和双引号的区别,单引号是完全字符串
    print 'what $b'."<br />";
    echo print("yes")."<br />";

    #print_r()也有返回值1
    print_r($a."<br />");
    print_r($c);
    echo "<br />";
    echo print_r("yes");   

?>