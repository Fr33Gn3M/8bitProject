<?php
echo "数组1正序：<br />";
$arr = ["text1","car1","name1"];
sort($arr);
for($x=0;$x<count($arr);$x++){
    echo $arr[$x];
    echo "<br />";
}

echo "数组1根据键正序排序：<br />";
ksort($arr);
for($x=0;$x<count($arr);$x++){
    echo $arr[$x];
    echo "<br />";
}

echo "数组1根据值正序排序：<br />";
asort($arr);
for($x=0;$x<count($arr);$x++){
    echo $arr[$x];
    echo "<br />";
}
echo "数组2逆序排序：";
$arr2 = array('a1' => 34, 'a2'=>35);
rsort($arr2);
foreach ($arr2 as $key => $value) {
    # code...
    echo $key . $value;
    echo "<br />";
}
echo "数组2键逆序排序： <br />";
krsort($arr2);
foreach ($arr2 as $key => $value) {
    # code...
    echo $key . $value;
    echo "<br />";
}
echo "数组2值逆序排序：<br />";
arsort($arr2);
foreach ($arr2 as $key => $value) {
    # code...
    echo $key . $value;
    echo "<br />";
}
?>