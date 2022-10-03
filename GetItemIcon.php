<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$itemID = $_POST["itemID"];

$path = "http://localhost/SqlTestProject/ItemIcons/".$itemID.".png";
$image = file_get_contents($path);

echo $image;

$conn->close();
?>