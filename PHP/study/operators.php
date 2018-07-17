<?php
    #常见的运算符都一致
    #=== 恒等于。如果 x 等于 y，且它们类型相同，则返回 true
    $a = 5 ==='5'; //false
    #<> 不等于。同== 
    $b = 5 <> '5'; //false
    #!== 不恒等于。如果 x 不等于 y，或它们类型不相同，则返回 true
    $c = 5 !== '5'; //true
    $d = 5 !== 6; //true
    #and 与。同&&
    $e = (5>1 and 6>3); //true
    #or 或。同||
    $f = (5<1 or 6>3);  //true
    #xor 异或。如果x和y有且仅有一个为true，则返回true
    $g = (5>2 xor 6<3); //true
    $h = (5>2 xor 6>3); //false
    $i = (5<2 xor 6<3); //false

    #数组运算符
    $arr = ["a","b","c"];
    $arr2 = array('0' => "a", '1'=>"b",'2'=>"c");
    $brr = array('x' => "a", 'y'=>"b",'z'=>"c");
    $crr[0] = 'a';
    $crr[1] = 'b';
    $crr[2] = 'c';
    $drr = array('1' => 1, '2'=>2,'3'=>3);
    $err = array('1' => "1", '2'=>"2",'3'=>"3");
    $frr = array('1' => 1, '3'=>3,'2'=>2);
    # + 集合
    $z = $arr+$brr;
    # == 相等
    $y2 = $arr == $arr2; //键的数字类型不同 true
    $y = $arr == $brr; //键不同 false
    $x = $arr == $crr; //定义方式不同 true
    $w = $drr == $frr; //顺序不同 true
    $v = $drr == $err; //类型不同 true
    $v2 = $err == $frr; //类型和顺序不同 true
    # === 恒等
    $u2 = $arr === $arr2; //键的数字类型不同 true
    $u = $arr === $brr; //键不同 false
    $t = $arr === $crr; //定义方式不同 false
    $s = $drr === $frr; //顺序不同 false
    $r = $drr === $err; //类型不同 false
    $r2 = $err === $frr; //类型和顺序不同 false
    # != 不相等 , <> 也是不相等
    $q2 = $arr != $arr2; //键的数字类型不同 false
    $q = $arr != $brr; //键不同 true
    $p = $arr != $crr; //定义方式不同 false
    $o = $drr != $frr; //顺序不同 false
    $n = $drr != $err; //类型不同 false
    $n2 = $err != $frr; //类型和顺序不同 false
    # !== 不恒等 
    $m2 = $arr !== $arr2; //键的数字类型不同 false
    $m = $arr !== $brr; //键不同 false
    $k = $arr !== $crr; //定义方式不同 false
    $j = $drr !== $frr; //顺序不同 true
    $j1 = $drr !== $err; //类型不同 true
    $k2 = $err !== $frr; //类型和顺序不同 true

    #text1 
    $aarr = ["1","0","1"];
    $barr = ["true","false","true"];
    $carr = [true,false,true];
    $darr = [1,0,1];
    $t1 = $aarr == $barr;   //false
    $t2 = $aarr === $barr;  //false
    $t3 = $aarr == $carr;   //true
    $t4 = $aarr === $carr;  //false
    $t5 = $barr == $carr;   //false
    $t6 = $barr === $carr;  //false
    $t19 = $darr == $aarr;  //true
    $t20 = $darr === $aarr; //false
    $t21 = $darr == $barr;  //false
    $t22 = $darr === $barr; //false
    $t23 = $darr == $carr;  //true
    $t24 = $darr === $carr; //false
    #text2
    $va = 1;
    $vb = true;
    $vc = "1";
    $vd = "true";

    $t7 = $va == $vb;   //true
    $t8 = $va === $vb;  //false
    $t9 = $va == $vc;   //true
    $t10 = $va === $vc; //false
    $t11 = $va == $vd;  //false
    $t12 = $va === $vd; //false
    $t13 = $vb == $vc;  //true
    $t14 = $vb === $vc; //false
    $t15 = $vb == $vd;  //true
    $t16 = $vb === $vd; //false
    $t17 = $vc == $vd;  //false
    $t18 = $vc === $vd; //false
?>