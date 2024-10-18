/**
 * Node attributes
 *
 * These are operational attributes that are tied to node logic.
 * They can be paired with OPC tags to interact with external data sources
 * and modify the node's state dynamically.
 */

/**
 * Attribute value type
 *
 * Attributes can accept various types of values such as strings, booleans,
 * numbers, or even arrays of these types.
 */
type AttributeValue = string | boolean | number | Array<AttributeValue>;

/**
 * Base attribute keys
 *
 * These are predefined base attributes that every node can have, such as visibility
 * and opacity, which help control how a node behaves or is displayed.
 */
type BaseAttributeKeys = 'visibility' | 'opacity';

/**
 * Base attributes
 *
 * Base attributes are a collection of key-value pairs where the key is one
 * of the base attribute keys (e.g., 'visibility', 'opacity') and the value
 * is a `NodeAttribute`.
 */
type BaseAttributes = Record<BaseAttributeKeys, NodeAttribute>;

/**
 * Node attribute type
 *
 * This interface defines an individual attribute of a node.
 *
 * @property value This is the current value of the attribute.
 * @property dataSource The data source that the attribute is tied to, which may dynamically
 *                      update the value (e.g., from an OPC tag or other data source).
 */
export interface NodeAttribute {
  value: AttributeValue | null;
  dataSource: string | null;
}

/**
 * Node attributes type
 *
 * A collection of node attributes. Each attribute is defined by a key and a value.
 * Base attributes like visibility and opacity are included, and other custom attributes
 * can be defined as needed.
 */
export type NodeAttributes = {
  [key: string]: NodeAttribute;
} & BaseAttributes;
