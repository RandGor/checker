<?
$name=$_GET['login'];
$pass=$_GET['pass'];
if(strlen($name)==strlen($pass)&&strlen($name)>0){
	echo("Вы вошли");
}else{
	echo("Вы не вошли");
}
?>