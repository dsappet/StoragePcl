# StoragePcl
Using PCLStorage library for data storage on mobile devices using xamarin. 

```C#
Load up the file
var storage = new StoragePCL.Storage(FileSystem.Current.LocalStorage);
await storage.LoadAsync();

// Write a value, this will create a key or overwrite an existing one. 
// Set the last parameter to true to encrypt the value before writing. Use this for things like passwords
await storage.InsertOrUpdateAsync("myKey", "This is the value.", false);

// Read in a value from key
var myValue = storage.Get("myKey");

// Get with an out for status code

// Delete a key

// Insert 

// Update only

// List out enum status codes
```
