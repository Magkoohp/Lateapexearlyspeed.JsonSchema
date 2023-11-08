﻿using System.Text.Json;

namespace JsonSchemaConsoleApp;

public class JsonValidator
{
    private readonly IJsonSchemaDocument _mainSchemaDoc;

    private readonly SchemaResourceRegistry _globalSchemaResourceRegistry = new();

    public JsonValidator(string jsonSchema)
    {
        _mainSchemaDoc = JsonSchemaDocument.Create(jsonSchema, _globalSchemaResourceRegistry);
    }

    public void AddExternalDocument(string externalJsonSchema)
    {
        JsonSchemaDocument.Create(externalJsonSchema, _globalSchemaResourceRegistry);
    }

    public ValidationResult Validate(string jsonInstance)
    {
        using (JsonDocument instance = JsonDocument.Parse(jsonInstance))
        {
            return _mainSchemaDoc.Validate(instance.RootElement);
        }
                
    }
}