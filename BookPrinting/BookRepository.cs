class BookRepository
{
    public function save(Book $book): void
    {
        $filename = '/documents/' .
            $book->getTitle() . ' - ' . $book->getAuthor();

    file_put_contents($filename, serialize($book));
    }
}