<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$itemID = $_POST["itemID"];
$userID = $_POST["userID"];

$sql = "SELECT price FROM items WHERE ID = '". $itemID."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    $itemPrice = $result ->fetch_assoc()["price"];
    $sql2 = "DELETE FROM usersitems WHERE itemID = '".$itemID."' AND userID = '".$userID."'";
    $result2 = $conn -> query($sql2);
    if($result2 === TRUE){
        $sql3 = "UPDATE users SET coins = coins + '".$itemPrice."' WHERE id = '".$userID."'";
        $conn ->query($sql3);
        echo " 판매완료";
    }
    else{
        echo "판매하려는 아이템이 인벤토리 DB에 존재하지 않습니다.";
    }
} else {
  echo "해당하는 ID의 아이템이 DB에 없습니다.";
}
$conn->close();
?>