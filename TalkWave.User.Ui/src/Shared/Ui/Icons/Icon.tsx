interface Props {
  path: string;
  alt: string;
  width: number;
  height: number;
}

export const Icon = ({ path, alt, width, height }: Props) => {
  return (
    <img
      src={path}
      alt={alt}
      style={{ width: width, height: height, color: "white" }}
    />
  );
};
