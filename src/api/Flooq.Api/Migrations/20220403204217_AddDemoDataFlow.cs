using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class AddDemoDataFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DataFlows",
                columns: new[] { "Id", "Definition", "LastEdited", "Name", "Status" },
                values: new object[] { new Guid("de780797-b556-4122-b133-7a446f79b024"), "{\n  \"nodes\": [\n    {\n      \"id\": \"1\",\n      \"dragHandle\": \".custom-drag-handle\",\n      \"type\": \"httpIn\",\n      \"data\": {\n        \"title\": \"Http Input\",\n        \"input\": {\n          \"url\": \"https://executor.dataflow.ch/IJF9K2\",\n          \"method\": \"post\",\n          \"contentType\": \"application/json\",\n          \"sampleBody\": \"{}\"\n        },\n        \"incomingHandles\": [],\n        \"outgoingHandles\": [\n          {\n            \"id\": \"11\",\n            \"name\": \"a\"\n          }\n        ]\n      },\n      \"position\": {\n        \"x\": 0,\n        \"y\": 0\n      }\n    },\n    {\n      \"id\": \"2\",\n      \"dragHandle\": \".custom-drag-handle\",\n      \"type\": \"filter\",\n      \"data\": {\n        \"title\": \"Filter\",\n        \"filter\": {\n          \"field\": \"tags\",\n          \"value\": \"secret\",\n          \"condition\": \"ne\"\n        },\n        \"incomingHandles\": [\n          {\n            \"id\": \"21\",\n            \"name\": \"a\"\n          }\n        ],\n        \"outgoingHandles\": [\n          {\n            \"id\": \"21\",\n            \"name\": \"a\"\n          }\n        ]\n      },\n      \"position\": {\n        \"x\": 400,\n        \"y\": 100\n      }\n    },\n    {\n      \"id\": \"3\",\n      \"dragHandle\": \".custom-drag-handle\",\n      \"type\": \"httpOut\",\n      \"data\": {\n        \"title\": \"Http Output\",\n        \"output\": {\n          \"url\": \"\",\n          \"method\": \"post\",\n          \"contentType\": \"application/json\",\n          \"sampleBody\": \"{}\"\n        },\n        \"incomingHandles\": [\n          {\n            \"id\": \"3a\",\n            \"name\": \"a\"\n          }\n        ],\n        \"outgoingHandles\": []\n      },\n      \"position\": {\n        \"x\": 800,\n        \"y\": 0\n      }\n    }\n  ],\n  \"edges\": [\n    {\n      \"id\": \"e1-2\",\n      \"fromNode\": \"1\",\n      \"fromHandle\": \"11\",\n      \"toNode\": \"2\",\n      \"toHandle\": \"2a\"\n    },\n    {\n      \"id\": \"e2-3\",\n      \"fromNode\": \"2\",\n      \"fromHandle\": \"21\",\n      \"toNode\": \"3\",\n      \"toHandle\": \"3a\"\n    }\n  ]\n}", new DateTime(2022, 3, 5, 13, 45, 12, 0, DateTimeKind.Utc), "Demo Flow #1", "Active" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataFlows",
                keyColumn: "Id",
                keyValue: new Guid("de780797-b556-4122-b133-7a446f79b024"));
        }
    }
}
