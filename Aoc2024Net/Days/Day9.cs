namespace Aoc2024Net.Days
{
    internal sealed class Day9 : Day
    {
        private abstract record Block(int Size);
        private record FileBlock(int Size, int Id) : Block(Size);
        private record FreeSpaceBlock(int Size) : Block(Size);

        public override object? SolvePart1() =>
            Solve(false);

        public override object? SolvePart2() =>
            Solve(true);

        private long Solve(bool moveWhole)
        {
            var blocks = GetBlocks();
            var filesCount = blocks.Count(b => b is FileBlock);

            for (var fileId = filesCount - 1; fileId >= 0; fileId--)
            {
                var fileNode = FindNode(blocks, node => node.Value is FileBlock fileBlock && fileBlock.Id == fileId);
                
                var file = (FileBlock)fileNode.Value;
                var fileSize = file.Size;

                while (fileSize > 0)
                {
                    var freeSpaceNode = FindNode(
                        blocks,
                        node => node.Value is FreeSpaceBlock && (!moveWhole || node.Value.Size >= file.Size),
                        node => node == fileNode);

                    if (freeSpaceNode == null)
                        break;

                    if (blocks.Contains(fileNode.Value))
                    {
                        var filePreviousNode = fileNode.Previous;
                        blocks.Remove(fileNode);
                        blocks.AddAfter(filePreviousNode, new FreeSpaceBlock(file.Size));
                    }

                    var freeSpaceSize = freeSpaceNode.Value.Size;
                    var cutSize = Math.Min(fileSize, freeSpaceSize);

                    var freeSpacePreviousNode = freeSpaceNode.Previous;
                    blocks.Remove(freeSpaceNode);
                    var newFileNode = blocks.AddAfter(freeSpacePreviousNode, new FileBlock(cutSize, file.Id));
                    if (freeSpaceSize - cutSize > 0)
                        blocks.AddAfter(newFileNode, new FreeSpaceBlock(freeSpaceSize - cutSize));

                    fileSize -= cutSize;
                }
            }

            return CalculateChecksum(blocks);
        }

        private long CalculateChecksum(LinkedList<Block> blocks)
        {
            var result = 0L;
            var position = 0;

            foreach (var block in blocks)
            {
                if (block is FreeSpaceBlock)
                {
                    position += block.Size;
                    continue;
                }

                for (var i = 0; i < block.Size; i++)
                {
                    result += position * ((FileBlock)block).Id;
                    position++;
                }
            }

            return result;
        }

        private LinkedListNode<Block>? FindNode(
            LinkedList<Block> blocks,
            Predicate<LinkedListNode<Block>> predicate,
            Predicate<LinkedListNode<Block>>? stopPredicate = null)
        {
            for (var node = blocks.First; node != null; node = node.Next)
            {
                if (stopPredicate?.Invoke(node) == true)
                    return null;

                if (predicate(node))
                    return node;
            }

            return null;
        }

        private LinkedList<Block> GetBlocks()
        {
            var text = InputData.GetInputText();

            var blocks = new LinkedList<Block>();

            var isFile = true;
            var fileId = 0;

            for (var i = 0; i < text.Length; i++)
            {
                var size = int.Parse(text[i].ToString());

                if (isFile)
                    blocks.AddLast(new FileBlock(size, fileId++));
                else
                    blocks.AddLast(new FreeSpaceBlock(size));

                isFile = !isFile;
            }

            return blocks;
        }
    }
}
