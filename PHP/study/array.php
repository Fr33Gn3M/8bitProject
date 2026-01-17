<?php
    #数值数组,两种定义方式
    $arr = array('a','b','c');
    $brr[0] = 'a';
    $brr[1] = 'b';
    $brr[2] = 'c';

    #下面这样的定义方式是允许的，但是不能调用$crr[1],会报错
    $crr[0] = 'a';
    $crr[2] = 'c';
    var_dump($arr);
    var_dump($brr);
    var_dump($crr);
    echo count($crr); //长度为2
    #遍历数值数组
    echo '数值数组：<br />';
    for($i=0;$i<count($arr);$i++)
    {
        echo $arr[$i];
        echo '<br />';
    }
    
    #关联数组
    echo '关联数组：<br />';
    $drr = array('name' => 'a', 'code'=> 'b', 'index'=> 'c');
    $err['name'] = 'a';
    $err['code'] = 'b';
    $err['index'] = 'c';
    foreach ($drr as $key => $value) {
        # code...
        echo 'key:'.$key.'. value:'.$value;
        echo '<br />';
    }
    echo '另一种遍历方式：<br />';
    foreach ($err as $value) {
        # code...
        echo $value;
        echo '<br />';
    }
    
?>