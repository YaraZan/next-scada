import type { Node } from '../node';
import type { NodeAttribute, NodeAttributes } from '../attributes';

type CheckboxAttributeKeys = 'relatedTag';
type LabelAttributes = Record<CheckboxAttributeKeys, NodeAttribute> & NodeAttributes;

export interface Checkbox extends Node {
  attributes?: LabelAttributes;
}
