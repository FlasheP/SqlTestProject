<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$itemID = $_POST["itemID"];

$sql = "SELECT name, description, price, imgVer FROM items WHERE ID = '". $itemID."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  $rows = array();
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  echo json_encode($rows);
} else {
  echo "해당하는 ID의 아이템이 DB에 없습니다.";
}
$conn->close();
?>