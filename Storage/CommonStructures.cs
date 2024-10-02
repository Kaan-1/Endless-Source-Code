using System.Collections.Generic;

public class OpenAIResponse{
    public List<Choice> Choices { get; set; }
}

public class Choice{
    public Message Message { get; set; }
}

public class Message{
    public string Role { get; set; }
    public string Content { get; set; }
}

public class CharacterResponse{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Response{ get; set; }
}