<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$sql = "SELECT username FROM users WHERE username = '". $loginUser."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "$loginUser 는 이미 존재하는 닉네임입니다.";
} else {
    echo "$loginUser 유저를 생성합니다.";
    $sql2 = "INSERT INTO users (username, password, level, coins) VALUES ('". $loginUser."','". $loginPass."', '1', '0')";

    if ($conn->query($sql2) === TRUE) {
        echo "생성완료";
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
}
$conn->close();
?>