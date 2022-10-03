<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$userID = $_POST["userID"];

$sql = "SELECT itemID FROM usersitems WHERE userID = '". $userID."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  $rows = array();
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  echo json_encode($rows);
} else {
  echo "데이터베이스가 비어있습니다.";
}
$conn->close();
?>