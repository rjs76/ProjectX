<?php
// connect to the database
$db = mysqli_connect('sql.njit.edu', 'mw288', '1k9L0X2z', 'mw288');
$username = $_POST['username'];
$data = array();

  	$query = "SELECT * FROM 491users WHERE Name='$username'";
  	$results = mysqli_query($db, $query);
  	if($results){
	while ($row = mysqli_fetch_array($results, MYSQLI_ASSOC)) {
	$data[]=(array("attack"=>$row["attack"],"defense"=>$row["defense"],"speed"=>$row["speed"],"stamina"=>$row["stamina"]));
}
}
echo json_encode($data);
?>