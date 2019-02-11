<?php
ini_set('max_execution_time', 0);
$urlsRaw = file_get_contents('https://pastebin.com/raw/PkapHr0D');
$urls = explode("\n", $urlsRaw);
$fp = fopen('dl-links.txt', 'a');
foreach ($urls as $url) {
	$url = trim($url);
	$dom = new DOMDocument();
	echo "Plugin link: ".$url."<br/>";
	$content = file_get_contents($url);
	@$dom->loadHTML($content);
	$links = $dom->getElementsByTagName('a');
	foreach ($links as $link) {
		foreach ($link->attributes as $attr) {
			$name = $attr->nodeName;
			$value = $attr->nodeValue;
			if ($name == 'href' && strpos($value, '.cs') !== FALSE) {
				echo "Download link: ".$value."<br/>";
				fwrite($fp, $value."\n");
			}
		}	
	}
	echo "<hr/>";
	sleep(1);
}
fclose($fp);
?>
