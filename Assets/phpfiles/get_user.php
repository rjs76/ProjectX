<?php
// connect to the database
$db = mysqli_connect('sql.njit.edu', 'username', 'password', 'tableName');
// check if login_user is posted
  $username = $_POST['username'];
  $password = &$_POST['password'];
  	$query = "SELECT * FROM Users WHERE Name='$username' AND Password='$password'";
  	$results = mysqli_query($db, $query);
  	if (mysqli_num_rows($results) == 1) {
		$row = mysqli_fetch_array($results, MYSQLI_ASSOC);
  	  $_SESSION['username'] = $username;
	  $_SESSION['type'] = $type;
  	  $_SESSION['success'] = "You are now logged in";
  	  $user = new \stdClass();
	  $user->username = $username;
	  $user->id = $row["ID"];
	  $user->playerRank = $row["playerRank"];
	  echo json_encode($user);
  	}else {
  		echo json_encode("No such user found in database");
  	}
?>