using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CatalogueProducts.Drupal
{
    public class FieldString
    {
        public string Value { get; set; }
    }

    public class FieldInt
    {
        public int Value { get; set; }
    }

    public class FieldLong
    {
        public long Value { get; set; }
    }

    public class FieldType
    {
        [JsonProperty("target_id")]
        public string Id { get; set; }
        [JsonProperty("target_label")]
        public string Label { get; set; }
    }

    public class FieldDate
    {
        public DateTime Value { get; set; }
    }

    public class Catalogue
    {
        [JsonProperty("nid")]
        public IEnumerable<FieldInt> Id { get; set; }
        public IEnumerable<FieldType> Type { get; set; }
        [JsonProperty("title")]
        public IEnumerable<FieldString> Name { get; set; }
        public IEnumerable<FieldInt> Changed { get; set; }
        [JsonProperty("revision_timestamp")]
        public IEnumerable<FieldLong> LastUpdated { get; set; }
        [JsonProperty("field_linked_milestones")] //TODO: change
        public IEnumerable<FieldProduct> Products { get; set; }
    }

    public class FieldProduct
    {
        [JsonProperty("target_id")]
        public int Id { get; set; }
        [JsonProperty("target_label")]
        public string Name { get; set; }
        public TargetProduct Target { get; set; }
    }

    public class TargetProduct
    {
        [JsonProperty("field_linked_vips")]//TODO: change
        public IEnumerable<FieldTarget> Seasons { get; set; }
    }

    public class FieldTarget
    {
        [JsonProperty("target_id")]
        public int Id { get; set; }
        [JsonProperty("target_type")]
        public string Type { get; set; }
        [JsonProperty("target_label")]
        public string Name { get; set; }
    }
}