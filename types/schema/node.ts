import type { NodeAttributes } from './attributes';
import type { NodeProperties } from './properties';

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
 * - Label: A text-based node.
 * - Image: A node for displaying images.
 * - Input: A node used for capturing user input (e.g., text fields).
 * - Checkbox: A node used for boolean (true/false) input.
 */
export type NodeType = 'Label' | 'Image' | 'Input' | 'Checkbox';

/**
 * Base schema node type
 *
 * This interface defines the basic structure of a schema node.
 * It contains properties and attributes used to define visual
 * and logical aspects of nodes within the system.
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
   * Properties that define visual aspects of the node. These can be
   * used to style the node using CSS-like properties (e.g., width, height).
   */
  properties?: NodeProperties;

  /**
   * Attributes that define the operational logic of the node.
   * These may be paired with some OPC tag to interact with external data sources.
   */
  attributes?: NodeAttributes;

  /**
   * The position of the node within the schema.
   */
  position?: NodePosition;
}
