namespace EntityModel.Test.Specifications
{
    public static class BNL_SPEC
    {
        public static readonly ISpecification<SCAudit> SPEC_NEWITEM = (BNL_SPEC.SPEC_NEWITEMESTEEM.AND(BNL_SPEC.SPEC_STAGE_E_OR_LTX));

        public static readonly ISpecification<SCAudit> SPEC_NEWITEMESTEEM = (BNL_SPEC_BASE.SPEC_PART_IS_BNL.AND(BNL_SPEC_BASE.SPEC_NEW_PO));
        public static readonly ISpecification<SCAudit> SPEC_STAGE_E_OR_LTX = (BNL_SPEC_BASE.SPEC_STAGE_E.OR(BNL_SPEC_BASE.SPEC_DESTINATION_LTX));

    }
}
