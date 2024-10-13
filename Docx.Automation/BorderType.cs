namespace Docx.Automation;

/// <summary>
/// Types of borders.
/// </summary>
public enum BorderType
{
  /// <summary>
  /// No Border.
  /// </summary>
  Nil,
  /// <summary>
  /// No Border.
  /// </summary>
  NoBorder,
  /// <summary>
  /// Single Line Border.
  /// </summary>
  Single,
  /// <summary>
  /// Single Line Border.
  /// </summary>
  Thick,
  /// <summary>
  /// Double Line Border.
  /// </summary>
  Double,
  /// <summary>
  /// Dotted Line Border.
  /// </summary>
  Dotted,
  /// <summary>
  /// Dashed Line Border.
  /// </summary>
  Dashed,
  /// <summary>
  /// Dot Dash Line Border.
  /// </summary>
  DotDash,
  /// <summary>
  /// Dot Dot Dash Line Border.
  /// </summary>
  DotDotDash,
  /// <summary>
  /// Triple Line Border.
  /// </summary>
  Triple,
  /// <summary>
  /// Thin, Thick Line Border.
  /// </summary>
  ThinThickSmallGap,
  /// <summary>
  /// Thick, Thin Line Border.
  /// </summary>
  ThickThinSmallGap,
  /// <summary>
  /// Thin, Thick, Thin Line Border.
  /// </summary>
  ThinThickThinSmallGap,
  /// <summary>
  /// Thin, Thick Line Border.
  /// </summary>
  ThinThickMediumGap,
  /// <summary>
  /// Thick, Thin Line Border.
  /// </summary>
  ThickThinMediumGap,
  /// <summary>
  /// Thin, Thick, Thin Line Border.
  /// </summary>
  ThinThickThinMediumGap,
  /// <summary>
  /// Thin, Thick Line Border.
  /// </summary>
  ThinThickLargeGap,
  /// <summary>
  /// Thick, Thin Line Border.
  /// </summary>
  ThickThinLargeGap,
  /// <summary>
  /// Thin, Thick, Thin Line Border.
  /// </summary>
  ThinThickThinLargeGap,
  /// <summary>
  /// Wavy Line Border.
  /// </summary>
  Wave,
  /// <summary>
  /// Double Wave Line Border.
  /// </summary>
  DoubleWave,
  /// <summary>
  /// Dashed Line Border.
  /// </summary>
  DashSmallGap,
  /// <summary>
  /// Dash Dot Strokes Line Border.
  /// </summary>
  DashDotStroked,
  /// <summary>
  /// 3D Embossed Line Border.
  /// </summary>
  ThreeDEmboss,
  /// <summary>
  /// 3D Engraved Line Border.
  /// </summary>
  ThreeDEngrave,
  /// <summary>
  /// Outset Line Border.
  /// </summary>
  Outset,
  /// <summary>
  /// Inset Line Border.
  /// </summary>
  Inset,
  /// <summary>
  /// Apples Art Border.
  /// </summary>
  Apples,
  /// <summary>
  /// Arched Scallops Art Border.
  /// </summary>
  ArchedScallops,
  /// <summary>
  /// Baby Pacifier Art Border.
  /// </summary>
  BabyPacifier,
  /// <summary>
  /// Baby Rattle Art Border.
  /// </summary>
  BabyRattle,
  /// <summary>
  /// Three Color Balloons Art Border.
  /// </summary>
  Balloons3Colors,
  /// <summary>
  /// Hot Air Balloons Art Border.
  /// </summary>
  BalloonsHotAir,
  /// <summary>
  /// Black Dash Art Border.
  /// </summary>
  BasicBlackDashes,
  /// <summary>
  /// Black Dot Art Border.
  /// </summary>
  BasicBlackDots,
  /// <summary>
  /// Black Square Art Border.
  /// </summary>
  BasicBlackSquares,
  /// <summary>
  /// Thin Line Art Border.
  /// </summary>
  BasicThinLines,
  /// <summary>
  /// White Dash Art Border.
  /// </summary>
  BasicWhiteDashes,
  /// <summary>
  /// White Dot Art Border.
  /// </summary>
  BasicWhiteDots,
  /// <summary>
  /// White Square Art Border.
  /// </summary>
  BasicWhiteSquares,
  /// <summary>
  /// Wide Inline Art Border.
  /// </summary>
  BasicWideInline,
  /// <summary>
  /// Wide Midline Art Border.
  /// </summary>
  BasicWideMidline,
  /// <summary>
  /// Wide Outline Art Border.
  /// </summary>
  BasicWideOutline,
  /// <summary>
  /// Bats Art Border.
  /// </summary>
  Bats,
  /// <summary>
  /// Birds Art Border.
  /// </summary>
  Birds,
  /// <summary>
  /// Birds Flying Art Border.
  /// </summary>
  BirdsFlight,
  /// <summary>
  /// Cabin Art Border.
  /// </summary>
  Cabins,
  /// <summary>
  /// Cake Art Border.
  /// </summary>
  CakeSlice,
  /// <summary>
  /// Candy Corn Art Border.
  /// </summary>
  CandyCorn,
  /// <summary>
  /// Knot Work Art Border.
  /// </summary>
  CelticKnotwork,
  /// <summary>
  /// Certificate Banner Art Border.
  /// </summary>
  CertificateBanner,
  /// <summary>
  /// Chain Link Art Border.
  /// </summary>
  ChainLink,
  /// <summary>
  /// Champagne Bottle Art Border.
  /// </summary>
  ChampagneBottle,
  /// <summary>
  /// Black and White Bar Art Border.
  /// </summary>
  CheckedBarBlack,
  /// <summary>
  /// Color Checked Bar Art Border.
  /// </summary>
  CheckedBarColor,
  /// <summary>
  /// Checkerboard Art Border.
  /// </summary>
  Checkered,
  /// <summary>
  /// Christmas Tree Art Border.
  /// </summary>
  ChristmasTree,
  /// <summary>
  /// Circles And Lines Art Border.
  /// </summary>
  CirclesLines,
  /// <summary>
  /// Circles and Rectangles Art Border.
  /// </summary>
  CirclesRectangles,
  /// <summary>
  /// Wave Art Border.
  /// </summary>
  ClassicalWave,
  /// <summary>
  /// Clocks Art Border.
  /// </summary>
  Clocks,
  /// <summary>
  /// Compass Art Border.
  /// </summary>
  Compass,
  /// <summary>
  /// Confetti Art Border.
  /// </summary>
  Confetti,
  /// <summary>
  /// Confetti Art Border.
  /// </summary>
  ConfettiGrays,
  /// <summary>
  /// Confetti Art Border.
  /// </summary>
  ConfettiOutline,
  /// <summary>
  /// Confetti Streamers Art Border.
  /// </summary>
  ConfettiStreamers,
  /// <summary>
  /// Confetti Art Border.
  /// </summary>
  ConfettiWhite,
  /// <summary>
  /// Corner Triangle Art Border.
  /// </summary>
  CornerTriangles,
  /// <summary>
  /// Dashed Line Art Border.
  /// </summary>
  CouponCutoutDashes,
  /// <summary>
  /// Dotted Line Art Border.
  /// </summary>
  CouponCutoutDots,
  /// <summary>
  /// Maze Art Border.
  /// </summary>
  CrazyMaze,
  /// <summary>
  /// Butterfly Art Border.
  /// </summary>
  CreaturesButterfly,
  /// <summary>
  /// Fish Art Border.
  /// </summary>
  CreaturesFish,
  /// <summary>
  /// Insects Art Border.
  /// </summary>
  CreaturesInsects,
  /// <summary>
  /// Ladybug Art Border.
  /// </summary>
  CreaturesLadyBug,
  /// <summary>
  /// Cross-stitch Art Border.
  /// </summary>
  CrossStitch,
  /// <summary>
  /// Cupid Art Border.
  /// </summary>
  Cup,
  /// <summary>
  /// Archway Art Border.
  /// </summary>
  DecoArch,
  /// <summary>
  /// Color Archway Art Border.
  /// </summary>
  DecoArchColor,
  /// <summary>
  /// Blocks Art Border.
  /// </summary>
  DecoBlocks,
  /// <summary>
  /// Gray Diamond Art Border.
  /// </summary>
  DiamondsGray,
  /// <summary>
  /// Double D Art Border.
  /// </summary>
  DoubleD,
  /// <summary>
  /// Diamond Art Border.
  /// </summary>
  DoubleDiamonds,
  /// <summary>
  /// Earth Art Border.
  /// </summary>
  Earth1,
  /// <summary>
  /// Earth Art Border.
  /// </summary>
  Earth2,
  /// <summary>
  /// Shadowed Square Art Border.
  /// </summary>
  EclipsingSquares1,
  /// <summary>
  /// Shadowed Square Art Border.
  /// </summary>
  EclipsingSquares2,
  /// <summary>
  /// Painted Egg Art Border.
  /// </summary>
  EggsBlack,
  /// <summary>
  /// Fans Art Border.
  /// </summary>
  Fans,
  /// <summary>
  /// Film Reel Art Border.
  /// </summary>
  Film,
  /// <summary>
  /// Firecracker Art Border.
  /// </summary>
  Firecrackers,
  /// <summary>
  /// Flowers Art Border.
  /// </summary>
  FlowersBlockPrint,
  /// <summary>
  /// Daisy Art Border.
  /// </summary>
  FlowersDaisies,
  /// <summary>
  /// Flowers Art Border.
  /// </summary>
  FlowersModern1,
  /// <summary>
  /// Flowers Art Border.
  /// </summary>
  FlowersModern2,
  /// <summary>
  /// Pansy Art Border.
  /// </summary>
  FlowersPansy,
  /// <summary>
  /// Red Rose Art Border.
  /// </summary>
  FlowersRedRose,
  /// <summary>
  /// Roses Art Border.
  /// </summary>
  FlowersRoses,
  /// <summary>
  /// Flowers in a Teacup Art Border.
  /// </summary>
  FlowersTeacup,
  /// <summary>
  /// Small Flower Art Border.
  /// </summary>
  FlowersTiny,
  /// <summary>
  /// Gems Art Border.
  /// </summary>
  Gems,
  /// <summary>
  /// Gingerbread Man Art Border.
  /// </summary>
  GingerbreadMan,
  /// <summary>
  /// Triangle Gradient Art Border.
  /// </summary>
  Gradient,
  /// <summary>
  /// Handmade Art Border.
  /// </summary>
  Handmade1,
  /// <summary>
  /// Handmade Art Border.
  /// </summary>
  Handmade2,
  /// <summary>
  /// Heart-Shaped Balloon Art Border.
  /// </summary>
  HeartBalloon,
  /// <summary>
  /// Gray Heart Art Border.
  /// </summary>
  HeartGray,
  /// <summary>
  /// Hearts Art Border.
  /// </summary>
  Hearts,
  /// <summary>
  /// Pattern Art Border.
  /// </summary>
  HeebieJeebies,
  /// <summary>
  /// Holly Art Border.
  /// </summary>
  Holly,
  /// <summary>
  /// House Art Border.
  /// </summary>
  HouseFunky,
  /// <summary>
  /// Circular Art Border.
  /// </summary>
  Hypnotic,
  /// <summary>
  /// Ice Cream Cone Art Border.
  /// </summary>
  IceCreamCones,
  /// <summary>
  /// Light Bulb Art Border.
  /// </summary>
  LightBulb,
  /// <summary>
  /// Lightning Art Border.
  /// </summary>
  Lightning1,
  /// <summary>
  /// Lightning Art Border.
  /// </summary>
  Lightning2,
  /// <summary>
  /// Map Pins Art Border.
  /// </summary>
  MapPins,
  /// <summary>
  /// Maple Leaf Art Border.
  /// </summary>
  MapleLeaf,
  /// <summary>
  /// Muffin Art Border.
  /// </summary>
  MapleMuffins,
  /// <summary>
  /// Marquee Art Border.
  /// </summary>
  Marquee,
  /// <summary>
  /// Marquee Art Border.
  /// </summary>
  MarqueeToothed,
  /// <summary>
  /// Moon Art Border.
  /// </summary>
  Moons,
  /// <summary>
  /// Mosaic Art Border.
  /// </summary>
  Mosaic,
  /// <summary>
  /// Musical Note Art Border.
  /// </summary>
  MusicNotes,
  /// <summary>
  /// Patterned Art Border.
  /// </summary>
  Northwest,
  /// <summary>
  /// Oval Art Border.
  /// </summary>
  Ovals,
  /// <summary>
  /// Package Art Border.
  /// </summary>
  Packages,
  /// <summary>
  /// Black Palm Tree Art Border.
  /// </summary>
  PalmsBlack,
  /// <summary>
  /// Color Palm Tree Art Border.
  /// </summary>
  PalmsColor,
  /// <summary>
  /// Paper Clip Art Border.
  /// </summary>
  PaperClips,
  /// <summary>
  /// Papyrus Art Border.
  /// </summary>
  Papyrus,
  /// <summary>
  /// Party Favor Art Border.
  /// </summary>
  PartyFavor,
  /// <summary>
  /// Party Glass Art Border.
  /// </summary>
  PartyGlass,
  /// <summary>
  /// Pencils Art Border.
  /// </summary>
  Pencils,
  /// <summary>
  /// Character Art Border.
  /// </summary>
  People,
  /// <summary>
  /// Waving Character Border.
  /// </summary>
  PeopleWaving,
  /// <summary>
  /// Character With Hat Art Border.
  /// </summary>
  PeopleHats,
  /// <summary>
  /// Poinsettia Art Border.
  /// </summary>
  Poinsettias,
  /// <summary>
  /// Postage Stamp Art Border.
  /// </summary>
  PostageStamp,
  /// <summary>
  /// Pumpkin Art Border.
  /// </summary>
  Pumpkin1,
  /// <summary>
  /// Push Pin Art Border.
  /// </summary>
  PushPinNote2,
  /// <summary>
  /// Push Pin Art Border.
  /// </summary>
  PushPinNote1,
  /// <summary>
  /// Pyramid Art Border.
  /// </summary>
  Pyramids,
  /// <summary>
  /// Pyramid Art Border.
  /// </summary>
  PyramidsAbove,
  /// <summary>
  /// Quadrants Art Border.
  /// </summary>
  Quadrants,
  /// <summary>
  /// Rings Art Border.
  /// </summary>
  Rings,
  /// <summary>
  /// Safari Art Border.
  /// </summary>
  Safari,
  /// <summary>
  /// Saw tooth Art Border.
  /// </summary>
  Sawtooth,
  /// <summary>
  /// Gray Saw tooth Art Border.
  /// </summary>
  SawtoothGray,
  /// <summary>
  /// Scared Cat Art Border.
  /// </summary>
  ScaredCat,
  /// <summary>
  /// Umbrella Art Border.
  /// </summary>
  Seattle,
  /// <summary>
  /// Shadowed Squares Art Border.
  /// </summary>
  ShadowedSquares,
  /// <summary>
  /// Shark Tooth Art Border.
  /// </summary>
  SharksTeeth,
  /// <summary>
  /// Bird Tracks Art Border.
  /// </summary>
  ShorebirdTracks,
  /// <summary>
  /// Rocket Art Border.
  /// </summary>
  Skyrocket,
  /// <summary>
  /// Snowflake Art Border.
  /// </summary>
  SnowflakeFancy,
  /// <summary>
  /// Snowflake Art Border.
  /// </summary>
  Snowflakes,
  /// <summary>
  /// Sombrero Art Border.
  /// </summary>
  Sombrero,
  /// <summary>
  /// Southwest-themed Art Border.
  /// </summary>
  Southwest,
  /// <summary>
  /// Stars Art Border.
  /// </summary>
  Stars,
  /// <summary>
  /// Stars On Top Art Border.
  /// </summary>
  StarsTop,
  /// <summary>
  /// 3-D Stars Art Border.
  /// </summary>
  Stars3d,
  /// <summary>
  /// Stars Art Border.
  /// </summary>
  StarsBlack,
  /// <summary>
  /// Stars With Shadows Art Border.
  /// </summary>
  StarsShadowed,
  /// <summary>
  /// Sun Art Border.
  /// </summary>
  Sun,
  /// <summary>
  /// Whirligig Art Border.
  /// </summary>
  Swirligig,
  /// <summary>
  /// Torn Paper Art Border.
  /// </summary>
  TornPaper,
  /// <summary>
  /// Black Torn Paper Art Border.
  /// </summary>
  TornPaperBlack,
  /// <summary>
  /// Tree Art Border.
  /// </summary>
  Trees,
  /// <summary>
  /// Triangle Art Border.
  /// </summary>
  TriangleParty,
  /// <summary>
  /// Triangles Art Border.
  /// </summary>
  Triangles,
  /// <summary>
  /// Tribal Art Border One.
  /// </summary>
  Tribal1,
  /// <summary>
  /// Tribal Art Border Two.
  /// </summary>
  Tribal2,
  /// <summary>
  /// Tribal Art Border Three.
  /// </summary>
  Tribal3,
  /// <summary>
  /// Tribal Art Border Four.
  /// </summary>
  Tribal4,
  /// <summary>
  /// Tribal Art Border Five.
  /// </summary>
  Tribal5,
  /// <summary>
  /// Tribal Art Border Six.
  /// </summary>
  Tribal6,
  /// <summary>
  /// triangle1.
  /// </summary>
  Triangle1,
  /// <summary>
  /// triangle2.
  /// </summary>
  Triangle2,
  /// <summary>
  /// triangleCircle1.
  /// </summary>
  TriangleCircle1,
  /// <summary>
  /// triangleCircle2.
  /// </summary>
  TriangleCircle2,
  /// <summary>
  /// shapes1.
  /// </summary>
  Shapes1,
  /// <summary>
  /// shapes2.
  /// </summary>
  Shapes2,
  /// <summary>
  /// Twisted Lines Art Border.
  /// </summary>
  TwistedLines1,
  /// <summary>
  /// Twisted Lines Art Border.
  /// </summary>
  TwistedLines2,
  /// <summary>
  /// Vine Art Border.
  /// </summary>
  Vine,
  /// <summary>
  /// Wavy Line Art Border.
  /// </summary>
  Waveline,
  /// <summary>
  /// Weaving Angles Art Border.
  /// </summary>
  WeavingAngles,
  /// <summary>
  /// Weaving Braid Art Border.
  /// </summary>
  WeavingBraid,
  /// <summary>
  /// Weaving Ribbon Art Border.
  /// </summary>
  WeavingRibbon,
  /// <summary>
  /// Weaving Strips Art Border.
  /// </summary>
  WeavingStrips,
  /// <summary>
  /// White Flowers Art Border.
  /// </summary>
  WhiteFlowers,
  /// <summary>
  /// Woodwork Art Border.
  /// </summary>
  Woodwork,
  /// <summary>
  /// Crisscross Art Border.
  /// </summary>
  XIllusions,
  /// <summary>
  /// Triangle Art Border.
  /// </summary>
  ZanyTriangles,
  /// <summary>
  /// Zigzag Art Border.
  /// </summary>
  ZigZag,
  /// <summary>
  /// Zigzag stitch.
  /// </summary>
  ZigZagStitch,
}