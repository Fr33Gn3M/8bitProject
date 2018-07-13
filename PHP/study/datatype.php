<?php
    #String
    function str()
    {
        $a = 'yes';
        $b = "no";
        $c = 'text $a';
        $d = "text $a";
        echo $a,"<br>",$b,"<br>",$c,"<br>",$d,"<br>";
        var_dump($a);
    }
    #Integer
    function int2()
    {
        $a = 1;
        var_dump($a);
        $b = -1;
        var_dump($b);
        $c = 0x8c;
        var_dump($c);
        $d=023;
        var_dump($d);
    }
    #Float
    function float2()
    {
        $a=10.034;
        var_dump($a);
        $b=4e2;
        var_dump($b);
        $c = 5e-3;
        var_dump($c);
    }
    #Boolean
    function bool2()
    {
        $a=true;
        var_dump($a);
        $b = false;
        var_dump($b);
    }
    #Array
    function array2()
    {
        $arr = array("a1","a2","a3");
        var_dump($arr);
    }
    #Object
    function object2()
    {
        class c
        {
            public $a;
            private $b;
            function c($b='yes')
            {
                $this->a = $b;
            }
            function what()
            {
                return $this->a;
            }
        };
        $cc = new c('no');
        $dd = get_object_vars($cc);
        foreach ($dd as $key => $value) {
            echo $key,'->',$value,"<br />";
            # code...
        }
        echo $cc->what();
    }
    #NULL
    function null2()
    {
        $a=NULL;
        #echo $a没有输出
        echo $a,"<br />";
        var_dump($a);
        $b = 'x';
        echo $b,"<br />";
        $b = NULL;
        var_dump($b);
    }
?>