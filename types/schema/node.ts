/**
 * Node position
 *
 * This type defines the position of a node in 3D space (x, y, z). It is used to
 * position the node within a schema or diagram.
 *
 * @property x The horizontal position.
 * @property y The vertical position.
 * @property z The depth position.
 */
type NodePosition = {
  x: number;
  y: number;
  z: number;
};

/**
 * Node types
 *
 * Defines the different types of nodes available in the system.
 * Each node type represents a different kind of content or behavior.
 *
 * - LabelView: A node for displaying text-based content (e.g., labels or text).
 * - TextInput: A node for capturing string input from users.
 * - NumberInput: A node for capturing numeric input from users.
 * - ImageView: A node for displaying images.
 * - BooleanInput: A node for capturing boolean (true/false) input.
 * - ArrayInput: A node for capturing and displaying array-based data.
 */
export type NodeType =
  | 'LabelView'
  | 'TextInput'
  | 'NumberInput'
  | 'ImageView'
  | 'BooleanInput'
  | 'ArrayInput';

/**
 * Allowed data types
 *
 * Defines the data types that a node attribute can store or interact with.
 * It supports primitive types and arrays of these types.
 */
export type AllowedDataTypes = string | boolean | number | AllowedDataTypes[];

/**
 * UIComponent
 *
 * Defines the types of UI components available to configure nodes.
 * These components represent interactive elements in the node's interface.
 */
export type UIComponent =
  | 'StringInput'
  | 'NumberInput'
  | 'BooleanToggle'
  | 'ArrayChart'
  | 'OptionPicker'
  | 'ColorPicker'
  | 'UploadField';

/**
 * NodeAspect
 *
 * Represents a configurable aspect of a node, such as a style or property.
 * Each aspect is linked to a UI component and can include optional measurement,
 * a specific value, and a set of options.
 *
 * @property component The UI component type used to configure this aspect.
 * @property measurement Optional unit of measurement (e.g., px, %, em).
 * @property value The current value of the aspect.
 * @property options Optional list of allowed values for the aspect.
 */
export interface NodeAspect {
  component: UIComponent;
  measurement?: string;
  value: AllowedDataTypes;
  options?: AllowedDataTypes[];
}

type NodeStyle = NodeAspect;

/**
 * Base style keys
 *
 * These are common style keys available to all nodes for basic styling and positioning,
 * such as width, height, visibility, and rotation.
 */
type BaseStyleKeys = 'width' | 'height';

/**
 * Base styles
 *
 * Base styles are a collection of key-value pairs where the key is one of the
 * base style keys (e.g., 'width', 'height') and the value is a `NodeAspect`.
 */
type BaseStyles = Record<BaseStyleKeys, NodeAspect>;

/**
 * NodeStyles
 *
 * Defines the styles applied to nodes, allowing them to be styled dynamically
 * based on external data. These styles follow a CSS-like structure.
 */
export type NodeStyles = {
  [key: string]: NodeStyle;
} & BaseStyles;

export type NodeProperty = NodeAspect & {
  dataSource?: string | null
};

type BasePropertyKeys = 'visibility' | 'rotation';

type BaseProperties = Record<BasePropertyKeys, NodeProperty>;

/**
 * NodeProperties
 *
 * Represents static properties of the node that define its behavior or
 * configuration, such as input values or placeholders.
 */
export type NodeProperties = {
  [key: string]: NodeProperty;
} & BaseProperties;

/**
 * Base schema node type
 *
 * This interface defines the basic structure of a schema node.
 * It contains properties, styles, and actions that define the visual,
 * logical, and interactive aspects of nodes within the system.
 */
export interface Node {
  /**
   * Unique identifier for the node.
   */
  uuid: string;

  /**
   * Human-readable name for the node.
   */
  name: string;

  /**
   * The type of the node, which defines its behavior and functionality.
   */
  type: NodeType;

  /**
   * Properties that define non-connectable visual aspects of the node.
   * These are fixed configurations, such as width, height, or other visual properties.
   */
  properties?: NodeProperties;

  /**
   * Styles that define the CSS-like styling of the node.
   * These are connectable to data sources, allowing dynamic changes
   * such as visibility, opacity, or positioning adjustments.
   */
  styles?: NodeStyles;

  /**
   * The position of the node within the schema or diagram.
   * This defines its placement in terms of x, y, and z coordinates.
   */
  position?: NodePosition;
}
