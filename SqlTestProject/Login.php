<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

//$sql = "SELECT password, id FROM users WHERE username = '". $loginUser."'";
//$result = $conn->query($sql);
//injection 예방위해 아래로 변경
$sql = "SELECT password, id FROM users WHERE username = ?";
$statement = $conn->prepare($sql);
$statement -> bind_param("s", $loginUser);
$statement->execute();
$result = $statement->get_result();

if ($result->num_rows > 0) {
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass){
      //유저 정보 가져오기
        echo $row["id"];
    }
    else{
        echo "wrong_password";
    }
  }
} else {
  echo "cant_find_username";
}
$conn->close();
?>