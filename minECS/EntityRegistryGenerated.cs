using System.Collections.Generic;
using System.Linq;
using EntIdx = System.Int32;
partial class EntityRegistry
{
public delegate void ProcessComponent<T0>(int entIdx, ref T0 component0);
public delegate void ProcessComponent<T0, T1>(int entIdx, ref T0 component0, ref T1 component1);
public delegate void ProcessComponent<T0, T1, T2>(int entIdx, ref T0 component0, ref T1 component1, ref T2 component2);
public delegate void ProcessComponent<T0, T1, T2, T3>(int entIdx, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3);
public delegate void ProcessComponent<T0, T1, T2, T3, T4>(int entIdx, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);
public void Loop<T0>(ProcessComponent<T0> loopAction)
where T0 : struct
{
  ushort typeMask = 0;
  var t0Base = componentsManager_.GetBufferSlow<T0>();
  if (t0Base.Sparse) typeMask |= 1 << 0;
  switch (typeMask) {
    case 0b0:
      Loop0(loopAction,
        (ComponentBufferDense<T0>)t0Base
      ); return;
    case 0b1:
      Loop1(loopAction,
        (ComponentBufferSparse<T0>)t0Base
      ); return;
  } // end switch (typeMask)
} // end function
private void Loop0<T0>(
ProcessComponent<T0> loopAction,
ComponentBufferDense<T0> t0B)
where T0 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
     loopAction(entIdx,     ref component0);
} // for components
} // Loop0
private void Loop1<T0>(
ProcessComponent<T0> loopAction,
ComponentBufferSparse<T0> t0B)
where T0 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
     loopAction(entIdx,     ref component0);
} // for components
} // Loop1


public void Loop<T0, T1>(ProcessComponent<T0, T1> loopAction)
where T0 : struct where T1 : struct
{
  ushort typeMask = 0;
  var t0Base = componentsManager_.GetBufferSlow<T0>();
  if (t0Base.Sparse) typeMask |= 1 << 0;
  var t1Base = componentsManager_.GetBufferSlow<T1>();
  if (t1Base.Sparse) typeMask |= 1 << 1;
  switch (typeMask) {
    case 0b00:
      Loop00(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base
      ); return;
    case 0b01:
      Loop01(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base
      ); return;
    case 0b10:
      Loop10(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base
      ); return;
    case 0b11:
      Loop11(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base
      ); return;
  } // end switch (typeMask)
} // end function
private void Loop00<T0, T1>(
ProcessComponent<T0, T1> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B)
where T0 : struct
where T1 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
     loopAction(entIdx,     ref component0,     ref component1);
}//end if flags test 2
} // for components
} // Loop00
private void Loop01<T0, T1>(
ProcessComponent<T0, T1> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B)
where T0 : struct
where T1 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
     loopAction(entIdx,     ref component0,     ref component1);
}//end if flags test 2
} // for components
} // Loop01
private void Loop10<T0, T1>(
ProcessComponent<T0, T1> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B)
where T0 : struct
where T1 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
     loopAction(entIdx,     ref component0,     ref component1);
}//end if flags test 2
} // for components
} // Loop10
private void Loop11<T0, T1>(
ProcessComponent<T0, T1> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B)
where T0 : struct
where T1 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
     loopAction(entIdx,     ref component0,     ref component1);
}//end if flags test 2
} // for components
} // Loop11


public void Loop<T0, T1, T2>(ProcessComponent<T0, T1, T2> loopAction)
where T0 : struct where T1 : struct where T2 : struct
{
  ushort typeMask = 0;
  var t0Base = componentsManager_.GetBufferSlow<T0>();
  if (t0Base.Sparse) typeMask |= 1 << 0;
  var t1Base = componentsManager_.GetBufferSlow<T1>();
  if (t1Base.Sparse) typeMask |= 1 << 1;
  var t2Base = componentsManager_.GetBufferSlow<T2>();
  if (t2Base.Sparse) typeMask |= 1 << 2;
  switch (typeMask) {
    case 0b000:
      Loop000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base
      ); return;
    case 0b001:
      Loop001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base
      ); return;
    case 0b010:
      Loop010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base
      ); return;
    case 0b011:
      Loop011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base
      ); return;
    case 0b100:
      Loop100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base
      ); return;
    case 0b101:
      Loop101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base
      ); return;
    case 0b110:
      Loop110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base
      ); return;
    case 0b111:
      Loop111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base
      ); return;
  } // end switch (typeMask)
} // end function
private void Loop000<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop000
private void Loop001<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop001
private void Loop010<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop010
private void Loop011<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop011
private void Loop100<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop100
private void Loop101<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop101
private void Loop110<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop110
private void Loop111<T0, T1, T2>(
ProcessComponent<T0, T1, T2> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B)
where T0 : struct
where T1 : struct
where T2 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2);
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop111


public void Loop<T0, T1, T2, T3>(ProcessComponent<T0, T1, T2, T3> loopAction)
where T0 : struct where T1 : struct where T2 : struct where T3 : struct
{
  ushort typeMask = 0;
  var t0Base = componentsManager_.GetBufferSlow<T0>();
  if (t0Base.Sparse) typeMask |= 1 << 0;
  var t1Base = componentsManager_.GetBufferSlow<T1>();
  if (t1Base.Sparse) typeMask |= 1 << 1;
  var t2Base = componentsManager_.GetBufferSlow<T2>();
  if (t2Base.Sparse) typeMask |= 1 << 2;
  var t3Base = componentsManager_.GetBufferSlow<T3>();
  if (t3Base.Sparse) typeMask |= 1 << 3;
  switch (typeMask) {
    case 0b0000:
      Loop0000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0001:
      Loop0001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0010:
      Loop0010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0011:
      Loop0011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0100:
      Loop0100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0101:
      Loop0101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0110:
      Loop0110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b0111:
      Loop0111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base
      ); return;
    case 0b1000:
      Loop1000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1001:
      Loop1001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1010:
      Loop1010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1011:
      Loop1011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1100:
      Loop1100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1101:
      Loop1101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1110:
      Loop1110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
    case 0b1111:
      Loop1111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base
      ); return;
  } // end switch (typeMask)
} // end function
private void Loop0000<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0000
private void Loop0001<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0001
private void Loop0010<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0010
private void Loop0011<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0011
private void Loop0100<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0100
private void Loop0101<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0101
private void Loop0110<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0110
private void Loop0111<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop0111
private void Loop1000<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1000
private void Loop1001<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1001
private void Loop1010<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1010
private void Loop1011<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1011
private void Loop1100<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1100
private void Loop1101<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1101
private void Loop1110<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1110
private void Loop1111<T0, T1, T2, T3>(
ProcessComponent<T0, T1, T2, T3> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3);
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop1111


public void Loop<T0, T1, T2, T3, T4>(ProcessComponent<T0, T1, T2, T3, T4> loopAction)
where T0 : struct where T1 : struct where T2 : struct where T3 : struct where T4 : struct
{
  ushort typeMask = 0;
  var t0Base = componentsManager_.GetBufferSlow<T0>();
  if (t0Base.Sparse) typeMask |= 1 << 0;
  var t1Base = componentsManager_.GetBufferSlow<T1>();
  if (t1Base.Sparse) typeMask |= 1 << 1;
  var t2Base = componentsManager_.GetBufferSlow<T2>();
  if (t2Base.Sparse) typeMask |= 1 << 2;
  var t3Base = componentsManager_.GetBufferSlow<T3>();
  if (t3Base.Sparse) typeMask |= 1 << 3;
  var t4Base = componentsManager_.GetBufferSlow<T4>();
  if (t4Base.Sparse) typeMask |= 1 << 4;
  switch (typeMask) {
    case 0b00000:
      Loop00000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00001:
      Loop00001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00010:
      Loop00010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00011:
      Loop00011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00100:
      Loop00100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00101:
      Loop00101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00110:
      Loop00110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b00111:
      Loop00111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01000:
      Loop01000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01001:
      Loop01001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01010:
      Loop01010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01011:
      Loop01011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01100:
      Loop01100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01101:
      Loop01101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01110:
      Loop01110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b01111:
      Loop01111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferDense<T4>)t4Base
      ); return;
    case 0b10000:
      Loop10000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10001:
      Loop10001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10010:
      Loop10010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10011:
      Loop10011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10100:
      Loop10100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10101:
      Loop10101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10110:
      Loop10110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b10111:
      Loop10111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferDense<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11000:
      Loop11000(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11001:
      Loop11001(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11010:
      Loop11010(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11011:
      Loop11011(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferDense<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11100:
      Loop11100(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11101:
      Loop11101(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferDense<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11110:
      Loop11110(loopAction,
        (ComponentBufferDense<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
    case 0b11111:
      Loop11111(loopAction,
        (ComponentBufferSparse<T0>)t0Base,
        (ComponentBufferSparse<T1>)t1Base,
        (ComponentBufferSparse<T2>)t2Base,
        (ComponentBufferSparse<T3>)t3Base,
        (ComponentBufferSparse<T4>)t4Base
      ); return;
  } // end switch (typeMask)
} // end function
private void Loop00000<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00000
private void Loop00001<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00001
private void Loop00010<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00010
private void Loop00011<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00011
private void Loop00100<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00100
private void Loop00101<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00101
private void Loop00110<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00110
private void Loop00111<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop00111
private void Loop01000<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01000
private void Loop01001<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01001
private void Loop01010<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01010
private void Loop01011<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01011
private void Loop01100<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01100
private void Loop01101<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01101
private void Loop01110<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01110
private void Loop01111<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferDense<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsDense & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop01111
private void Loop10000<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10000
private void Loop10001<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10001
private void Loop10010<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10010
private void Loop10011<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10011
private void Loop10100<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10100
private void Loop10101<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10101
private void Loop10110<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10110
private void Loop10111<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferDense<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsDense & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop10111
private void Loop11000<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11000
private void Loop11001<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11001
private void Loop11010<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11010
private void Loop11011<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferDense<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsDense & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11011
private void Loop11100<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11100
private void Loop11101<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferDense<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsDense & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11101
private void Loop11110<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferDense<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11110
private void Loop11111<T0, T1, T2, T3, T4>(
ProcessComponent<T0, T1, T2, T3, T4> loopAction,
ComponentBufferSparse<T0> t0B,
ComponentBufferSparse<T1> t1B,
ComponentBufferSparse<T2> t2B,
ComponentBufferSparse<T3> t3B,
ComponentBufferSparse<T4> t4B)
where T0 : struct
where T1 : struct
where T2 : struct
where T3 : struct
where T4 : struct
{
  var compBuffers = t0B.__GetBuffers();
  var compIdx2EntIdx = compBuffers.i2EntIdx;
  var components = compBuffers.data;
  var matcher1Flag = t1B.Matcher.Flag;
  var matcher1Buffers = t1B.__GetBuffers();
  var matcher2Flag = t2B.Matcher.Flag;
  var matcher2Buffers = t2B.__GetBuffers();
  var matcher3Flag = t3B.Matcher.Flag;
  var matcher3Buffers = t3B.__GetBuffers();
  var matcher4Flag = t4B.Matcher.Flag;
  var matcher4Buffers = t4B.__GetBuffers();
  for (var i = components.Length - 1; i >= 0; i--) {
    ref T0 component0 = ref components[i];
    EntIdx entIdx = compIdx2EntIdx[i];
    ref EntityData entityData = ref data_[entIdx];
  if ((entityData.FlagsSparse & matcher1Flag) != 0){
    int indexInMatcher1 = matcher1Buffers.entIdx2i[entIdx];
    ref T1 component1 = ref matcher1Buffers.data[indexInMatcher1];
  if ((entityData.FlagsSparse & matcher2Flag) != 0){
    int indexInMatcher2 = matcher2Buffers.entIdx2i[entIdx];
    ref T2 component2 = ref matcher2Buffers.data[indexInMatcher2];
  if ((entityData.FlagsSparse & matcher3Flag) != 0){
    int indexInMatcher3 = matcher3Buffers.entIdx2i[entIdx];
    ref T3 component3 = ref matcher3Buffers.data[indexInMatcher3];
  if ((entityData.FlagsSparse & matcher4Flag) != 0){
    int indexInMatcher4 = matcher4Buffers.entIdx2i[entIdx];
    ref T4 component4 = ref matcher4Buffers.data[indexInMatcher4];
     loopAction(entIdx,     ref component0,     ref component1,     ref component2,     ref component3,     ref component4);
}//end if flags test 5
}//end if flags test 4
}//end if flags test 3
}//end if flags test 2
} // for components
} // Loop11111



}
