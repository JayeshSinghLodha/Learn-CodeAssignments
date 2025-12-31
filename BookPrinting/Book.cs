class Book
{
    private string $title;
    private string $author;
    private int $currentPage = 1;

    public function __construct(string $title, string $author)
    {
        $this->title = $title;
        $this->author = $author;
    }

    public function getTitle(): string
    {
        return $this->title;
    }

public function getAuthor(): string
    {
        return $this->author;
    }

    public function turnPage(): void
    {
        $this->currentPage++;
    }

    public function getCurrentPage(): string
    {
        return "Content of page {$this->currentPage}";
    }
}
