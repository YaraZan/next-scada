/**
 * Node properties
 *
 * These are visual properties of a node that cannot be paired with an OPC tag.
 * Properties are typically used to style the node using CSS-like values (e.g., width, height).
 */

/**
 * Property value type
 *
 * Defines the possible value types for a node's property. It can be a string,
 * boolean, number, or an array of these types.
 */
type PropertyValue = string | boolean | number | Array<PropertyValue> | null;

/**
 * Property type
 *
 * Defines the types of inputs or components that can be used for configuring
 * a node's property (e.g., a text input field, a color picker).
 */
type PropertyType =
  | 'TextInput'
  | 'NumberInput'
  | 'BooleanToggle'
  | 'ArrayChart'
  | 'OptionPicker'
  | 'ColorPicker'
  | 'UploadField';

/**
 * Base property keys
 *
 * These are common property keys available to all nodes for basic styling and positioning,
 * such as width, height, visibility, and rotation.
 */
type BasePropertyKeys = 'width' | 'height' | 'visibility' | 'rotation';

/**
 * Base properties
 *
 * Base properties are a collection of key-value pairs where the key is one of the
 * base property keys (e.g., 'width', 'height') and the value is a `NodeProperty`.
 */
type BaseProperties = Record<BasePropertyKeys, NodeProperty>;

/**
 * Node property
 *
 * This interface defines an individual property of a node. Properties are visual
 * aspects of a node and are not linked to external data sources.
 *
 * @property type The type of input used to modify this property (e.g., TextInput, ColorPicker).
 * @property measurement (Optional) The measurement unit for the property (e.g., px, %).
 * @property value The value of the property.
 * @property options (Optional) A list of possible values for the property, used in selection inputs.
 */
export interface NodeProperty {
  type: PropertyType;
  measurement?: string;
  value: PropertyValue;
  options?: Array<PropertyValue>;
}

/**
 * Node properties type
 *
 * A collection of node properties. Each property is defined by a key and a value.
 * Base properties like width and height are included, and other custom properties
 * can be defined as needed.
 */
export type NodeProperties = {
  [key: string]: NodeProperty;
} & BaseProperties;
