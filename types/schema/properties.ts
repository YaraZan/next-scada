/**
 * Node property
 *
 * Type shall be returned from JSON decoded `properties`.
 */
export interface NodeProperty {
  measurement?: string | null;
  value: PropertyValue;
  dataSource?: string | null;
};

/**
 * Property value type
 *
 * Defines node trigger handling based on property type.
 */
type PropertyValue = string | boolean | number | Array<PropertyValue> | null;

type BasePropertyKeys = 'width' | 'height';

/**
 * Base properties for all nodes
 */
type BaseProperties = Record<BasePropertyKeys, NodeProperty>;

/**
 * Node properties type
 */
export type NodeProperties = {
  [key: string]: NodeProperty;
} & BaseProperties;

