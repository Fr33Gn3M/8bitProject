<?php
$a = "Hello";
$b = $a . " world!";
echo $b; // outputs Hello world! 

echo "<br />";

$y = 234;
echo $y."<br />";

$arr = ["text1","car1","name1"];
for($x=0;$x<count($arr);$x++){
    echo $arr[$x];
    echo "<br />";
}
$arr2 = array('a1' => 34, 'a2'=>35);
foreach ($arr2 as $key => $value) {
    # code...
    echo $key . $value;
    echo "<br />";
}
?>