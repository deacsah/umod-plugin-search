<?php 
ini_set('max_execution_time', 0);
$urlsRaw = file_get_contents('dl-links.txt');
$urls = explode("\n", $urlsRaw);
foreach ($urls as $url) {
	$url = trim($url);
	$name = str_replace("https://umod.org/plugins/", "", $url);
	if (!file_exists("plugins/".$name)) {
		$content = file_get_contents($url);
		$fp = fopen("plugins/".$name, 'w');
		fwrite($fp, $content);
		fclose($fp);
	}
	sleep(1);
}
?>
