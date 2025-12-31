interface Printer
{
    public function printPage(string $page): void;
}

class PlainTextPrinter implements Printer
{
    public function printPage(string $page): void
    {
        echo $page;
    }
}

class HtmlPrinter implements Printer
{
    public function printPage(string $page): void
    {
        echo "<div class='single-page'>{$page}</div>";
    }
}
