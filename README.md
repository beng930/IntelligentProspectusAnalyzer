# IntelligentProspectusAnalyzer
This is a playground project for leveraging LLM capabilities creating a smart chat bot, able to answer relevant question about large text files (specifically prospectuses).

The program goes through 2 main stages:
1. The file is saved in a blob, and is being chunked into overlapping pieces.
2. The chat bot is initialized with the relevant context, while the best fitting chunks from the file are added given every question presented in the chat, for the bot to answer as accurately as possible (given the assumption that we don't wish to send the whole file as the context with every call).

This approach can be viewed by the below general design:
![image](https://github.com/beng930/IntelligentProspectusAnalyzer/assets/39993978/f36ca305-5e1e-46bb-927a-32ed5760edd1)

Prequisits:
1. Azure subscription and the following resources - Azure OpenAI, Azure blob (storage account), Azure Cognitive Search.

You can use the logic in FilePartitionService to chunk your file into a set numbner of files, with some overlapping to keep the context. There are much better algorithms that can be used for this task, or even some Azure resources that could do it for you.
Notice that the secrets are saved in the code as consts (you should put your own to make it work), this is a bad practice for production services, and they should be stored safely in a Key Vault (or any other secret management tool).
