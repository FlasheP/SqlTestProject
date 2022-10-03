<?php
require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "데이터베이스 연결완료. 유저 정보를 출력합니다."."<br><br>";

$sql = "SELECT username, level FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "username: " . $row['username']. " - level: " . $row['level']. "<br>";
  }
} else {
  echo "데이터베이스가 비어있습니다.";
}
$conn->close();
?>