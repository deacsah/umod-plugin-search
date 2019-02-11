<?php
ini_set('max_execution_time', 0);
$plugins = scandir('plugins');
foreach ($plugins as $plugin) {
    if (!in_array($plugin, ['.','..','index.php'])) {
        $fs=filesize("plugins/".$plugin);
        if ($fs > 0) {
            echo "Plugin <strong>$plugin</strong> file size: $fs <br/>";
        } else {
            $contents = file_get_contents("https://umod.org/plugins/".$plugin);
            if (empty($contents)) {
                echo "Plugin <strong>$plugin</strong> was downloaded empty :( <br/>";
            } else {
                unlink("plugins/".$plugin);
                $fp = fopen("plugins/".$plugin, 'w');
                fwrite($fp, $contents);
                fclose($fp);
                echo "Plugin <strong>$plugin</strong> was succesfully re-downloaded :) <br/>";
            }
            sleep(2); // used to throttle the amount of requests
        }
    }
}
?>
