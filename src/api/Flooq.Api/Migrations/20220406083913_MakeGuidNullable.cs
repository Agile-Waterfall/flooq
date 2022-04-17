﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class MakeGuidNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataFlows",
                keyColumn: "Id",
                keyValue: new Guid("c45ec675-062b-452b-8c61-a2523f9840fc"));

            migrationBuilder.InsertData(
                table: "DataFlows",
                columns: new[] { "Id", "Definition", "Name", "Status" },
                values: new object[] { new Guid("e3d85d5d-7c27-4949-8ddc-a176bcce8843"), "{\r\n  \"nodes\": [\r\n    {\r\n      \"id\": \"1\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"httpIn\",\r\n      \"data\": {\r\n        \"title\": \"Http Input\",\r\n        \"input\": {\r\n          \"url\": \"https://executor.dataflow.ch/IJF9K2\",\r\n          \"method\": \"post\",\r\n          \"contentType\": \"application/json\",\r\n          \"sampleBody\": \"{}\"\r\n        },\r\n        \"incomingHandles\": [],\r\n        \"outgoingHandles\": [\r\n          {\r\n            \"id\": \"11\",\r\n            \"name\": \"a\"\r\n          }\r\n        ]\r\n      },\r\n      \"position\": {\r\n        \"x\": 0,\r\n        \"y\": 0\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"2\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"filter\",\r\n      \"data\": {\r\n        \"title\": \"Filter\",\r\n        \"filter\": {\r\n          \"field\": \"tags\",\r\n          \"value\": \"secret\",\r\n          \"condition\": \"ne\"\r\n        },\r\n        \"incomingHandles\": [\r\n          {\r\n            \"id\": \"21\",\r\n            \"name\": \"a\"\r\n          }\r\n        ],\r\n        \"outgoingHandles\": [\r\n          {\r\n            \"id\": \"21\",\r\n            \"name\": \"a\"\r\n          }\r\n        ]\r\n      },\r\n      \"position\": {\r\n        \"x\": 400,\r\n        \"y\": 100\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"3\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"httpOut\",\r\n      \"data\": {\r\n        \"title\": \"Http Output\",\r\n        \"output\": {\r\n          \"url\": \"\",\r\n          \"method\": \"post\",\r\n          \"contentType\": \"application/json\",\r\n          \"sampleBody\": \"{}\"\r\n        },\r\n        \"incomingHandles\": [\r\n          {\r\n            \"id\": \"3a\",\r\n            \"name\": \"a\"\r\n          }\r\n        ],\r\n        \"outgoingHandles\": []\r\n      },\r\n      \"position\": {\r\n        \"x\": 800,\r\n        \"y\": 0\r\n      }\r\n    }\r\n  ],\r\n  \"edges\": [\r\n    {\r\n      \"id\": \"e1-2\",\r\n      \"fromNode\": \"1\",\r\n      \"fromHandle\": \"11\",\r\n      \"toNode\": \"2\",\r\n      \"toHandle\": \"2a\"\r\n    },\r\n    {\r\n      \"id\": \"e2-3\",\r\n      \"fromNode\": \"2\",\r\n      \"fromHandle\": \"21\",\r\n      \"toNode\": \"3\",\r\n      \"toHandle\": \"3a\"\r\n    }\r\n  ]\r\n}", "Demo Flow #1", "Active" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataFlows",
                keyColumn: "Id",
                keyValue: new Guid("e3d85d5d-7c27-4949-8ddc-a176bcce8843"));

            migrationBuilder.InsertData(
                table: "DataFlows",
                columns: new[] { "Id", "Definition", "Name", "Status" },
                values: new object[] { new Guid("c45ec675-062b-452b-8c61-a2523f9840fc"), "{\r\n  \"nodes\": [\r\n    {\r\n      \"id\": \"1\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"httpIn\",\r\n      \"data\": {\r\n        \"title\": \"Http Input\",\r\n        \"input\": {\r\n          \"url\": \"https://executor.dataflow.ch/IJF9K2\",\r\n          \"method\": \"post\",\r\n          \"contentType\": \"application/json\",\r\n          \"sampleBody\": \"{}\"\r\n        },\r\n        \"incomingHandles\": [],\r\n        \"outgoingHandles\": [\r\n          {\r\n            \"id\": \"11\",\r\n            \"name\": \"a\"\r\n          }\r\n        ]\r\n      },\r\n      \"position\": {\r\n        \"x\": 0,\r\n        \"y\": 0\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"2\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"filter\",\r\n      \"data\": {\r\n        \"title\": \"Filter\",\r\n        \"filter\": {\r\n          \"field\": \"tags\",\r\n          \"value\": \"secret\",\r\n          \"condition\": \"ne\"\r\n        },\r\n        \"incomingHandles\": [\r\n          {\r\n            \"id\": \"21\",\r\n            \"name\": \"a\"\r\n          }\r\n        ],\r\n        \"outgoingHandles\": [\r\n          {\r\n            \"id\": \"21\",\r\n            \"name\": \"a\"\r\n          }\r\n        ]\r\n      },\r\n      \"position\": {\r\n        \"x\": 400,\r\n        \"y\": 100\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"3\",\r\n      \"dragHandle\": \".custom-drag-handle\",\r\n      \"type\": \"httpOut\",\r\n      \"data\": {\r\n        \"title\": \"Http Output\",\r\n        \"output\": {\r\n          \"url\": \"\",\r\n          \"method\": \"post\",\r\n          \"contentType\": \"application/json\",\r\n          \"sampleBody\": \"{}\"\r\n        },\r\n        \"incomingHandles\": [\r\n          {\r\n            \"id\": \"3a\",\r\n            \"name\": \"a\"\r\n          }\r\n        ],\r\n        \"outgoingHandles\": []\r\n      },\r\n      \"position\": {\r\n        \"x\": 800,\r\n        \"y\": 0\r\n      }\r\n    }\r\n  ],\r\n  \"edges\": [\r\n    {\r\n      \"id\": \"e1-2\",\r\n      \"fromNode\": \"1\",\r\n      \"fromHandle\": \"11\",\r\n      \"toNode\": \"2\",\r\n      \"toHandle\": \"2a\"\r\n    },\r\n    {\r\n      \"id\": \"e2-3\",\r\n      \"fromNode\": \"2\",\r\n      \"fromHandle\": \"21\",\r\n      \"toNode\": \"3\",\r\n      \"toHandle\": \"3a\"\r\n    }\r\n  ]\r\n}", "Demo Flow #1", "Active" });
        }
    }
}
